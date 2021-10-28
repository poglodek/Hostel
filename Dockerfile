#ARGS
ARG PortHttp=80
ARG PortHttps=443


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE ${PortHttp} ${PortHttps}
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Hostel System/Hostel System.csproj","Hostel System/"]
RUN dotnet restore "Hostel System/Hostel System.csproj"
COPY . .
WORKDIR "/src/Hostel System"
RUN dotnet build "Hostel System.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hostel System.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Hostel System.dll"]

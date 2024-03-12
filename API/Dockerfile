FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PeopleImportAPI/Adapter.API.csproj", "PeopleImportAPI/"]
RUN dotnet restore "./PeopleImportAPI/Adapter.API.csproj"
COPY . .
WORKDIR "/src/PeopleImportAPI"

RUN dotnet user-secrets init
RUN dotnet user-secrets set "PrivateKey" "lJOz{FaHnD220-[~Ne$wx8[%&h,T}d[;WSJfsw5a@pYC9R9vJP" --project"Adapter.API.csproj"
RUN dotnet user-secrets set "MongoDBConn" "mongodb://localhost:27017" --project "Adapter.API.csproj"
RUN dotnet user-secrets set "MongoDB" "PeopleImport" --project "Adapter.API.csproj"

RUN dotnet build "./Adapter.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Adapter.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adapter.API.dll",  "--urls", "https://*:8080"]
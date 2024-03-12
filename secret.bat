dotnet user-secrets init
dotnet user-secrets set "PrivateKey" "lJOz{FaHnD220-[~Ne$wx8[%&h,T}d[;WSJfsw5a@pYC9R9vJP" --project "API\Adapter.API.csproj"
dotnet user-secrets set "MongoDBConn" "mongodb://localhost:27017" --project "API\Adapter.API.csproj"
dotnet user-secrets set "MongoDB" "PeopleImport" --project "API\Adapter.API.csproj"
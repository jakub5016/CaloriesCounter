echo "{  
    \"Logging\": { 
        \"LogLevel\": { 
            \"Default\": \"Information\", 
            \"Microsoft.AspNetCore\": \"Warning\"
        }
    },
    \"AllowedHosts\": \"*\",
        \"ConnectionStrings\": {
            \"CaloriesCounterAPIContext\": \"server=db;port=5432;username=admin;password=admin;database=CaloriesCounterAPIContext-d03a527c-85d6-4eab-9ebd-51f977a70b0f\"
            }
    }" > ./appsettings.json

rm -r Migrations

dotnet-ef migrations add INIT

dotnet-ef database update

dotnet build

dotnet dev-certs https
dotnet dev-certs https -ep /usr/local/share/ca-certificates/aspnet/https.crt --format PEM
update-ca-certificates

dotnet run 
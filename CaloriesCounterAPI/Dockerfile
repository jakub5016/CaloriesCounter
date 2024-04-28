# Start with the ASP.NET base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY . .

# Generate appsettings.json with connection string
RUN echo "{ \
    \"Logging\": { \
        \"LogLevel\": { \
            \"Default\": \"Information\", \
            \"Microsoft.AspNetCore\": \"Warning\" \
        } \
    }, \
    \"AllowedHosts\": \"*\", \
    \"ConnectionStrings\": { \
        \"CaloriesCounterAPIContext\": \"server=db;port=5432;username=admin;password=admin;database=CaloriesCounterAPIContext-d03a527c-85d6-4eab-9ebd-51f977a70b0f\" \
    } \
}" > appsettings.json

# Build the application
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Expose port 80 for the ASP.NET application
EXPOSE 7261

# Set the entry point to run the ASP.NET application
COPY setup.sh .
RUN chmod +x setup.sh
ENTRYPOINT ["./setup.sh"]

FROM mcr.microsoft.com/dotnet/sdk:8.0	

RUN mkdir backend

COPY . backend
WORKDIR backend

RUN dotnet dev-certs https
RUN dotnet dev-certs https --trust


RUN dotnet build

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

#Menage database 
RUN dotnet-ef migrations add INIT
RUN dotnet-ef database update

CMD [ "dotnet run --urls https://localhost:7261" ]
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# EXPOSE 8080

# # Use the SDK image to build the application
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src
# COPY ["CaloriesCouter/CaloriesCouter.csproj", "CaloriesCouter/"]
# RUN dotnet restore "CaloriesCouter/CaloriesCouter.csproj"
# COPY . .
# WORKDIR "/src/CaloriesCouter"
# RUN dotnet build "CaloriesCouter.csproj" -c Release -o /app/build

# # Publish the application
# FROM build AS publish
# RUN dotnet publish "CaloriesCouter.csproj" -c Release -o /app/publish

# # Create the final runtime image
# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "CaloriesCouter.dll"]
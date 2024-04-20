FROM mcr.microsoft.com/dotnet/sdk:8.0	

RUN mkdir backend

COPY . backend
WORKDIR backend

RUN dotnet build

CMD [ "dotnet", "run" ]

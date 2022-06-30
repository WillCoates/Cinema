# This file is only used for building the entire solution
# Each microservice contains its own Dockerfile to extract files for deployment
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

COPY Cinema.sln */*/*/*.csproj .
COPY docker-utils ./docker-utils
RUN ./docker-utils/rebuild-structure
RUN dotnet restore

COPY . .
RUN dotnet publish --configuration Release

ENTRYPOINT ["true"]


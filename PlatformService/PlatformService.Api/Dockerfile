FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY PlatformService.Api/*.csproj ./PlatformService.Api/
COPY PlatformService.Data/*.csproj ./PlatformService.Data/
RUN dotnet restore

# copy everything else and build app
COPY PlatformService.Api/. ./PlatformService.Api/
COPY PlatformService.Data/. ./PlatformService.Data/
WORKDIR /app/PlatformService.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/PlatformService.Api/out ./
ENTRYPOINT ["dotnet", "PlatformService.dll"]
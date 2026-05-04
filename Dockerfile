FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution
COPY . .

# Install Node
RUN apt-get update && apt-get install -y nodejs npm

# Build Angular
WORKDIR /src/SITS.BNS.Server/ClientApp
RUN npm install
RUN npm run build -- --configuration production

# Build .NET
WORKDIR /src
RUN dotnet publish SITS.BNS.Server/SITS.BNS.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published backend
COPY --from=build /app/publish .

# IMPORTANT: Copy Angular build into wwwroot
COPY --from=build /src/SITS.BNS.Server/ClientApp/dist/sits.bns.client/browser ./wwwroot

ENTRYPOINT ["dotnet", "SITS.BNS.dll"]
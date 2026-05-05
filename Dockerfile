FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

COPY publish/ ./

ENTRYPOINT ["dotnet", "SITS.BNS.dll"]
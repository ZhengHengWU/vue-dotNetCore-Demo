FROM microsoft/dotnet:2.2-aspnetcore-runtime
FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /app
COPY . /app
WORKDIR /app
ENV ASPNETCORE_URLS http://+:5002
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 5002
 
CMD  ["dotnet", "Api.dll"]
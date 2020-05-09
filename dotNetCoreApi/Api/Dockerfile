FROM microsoft/dotnet:2.2-aspnetcore-runtime
FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /app
COPY . /app
WORKDIR /app
ENV ASPNETCORE_URLS http://+:5002
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 5002
#ENV certPassword 123456
#RUN openssl genrsa -des3 -passout pass:${certPassword} -out server.key 2048
#RUN openssl rsa -passin pass:${certPassword} -in server.key -out server.key
#RUN openssl req -sha256 -new -key server.key -out server.csr -subj '/CN=wuzhd'
#RUN openssl x509 -req -sha256 -days 365 -in server.csr -signkey server.key -out server.crt
#RUN openssl pkcs12 -export -out server.pfx -inkey server.key -in server.crt -certfile server.crt -passout pass:${certPassword}
CMD  ["dotnet", "Api.dll"]
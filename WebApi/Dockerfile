# Dockerfile para .NET WebApi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["BLL/BLL.csproj", "BLL/"]
COPY ["TRANSVERSAL/TRANSVERSAL.csproj", "TRANSVERSAL/"]
COPY . .
RUN dotnet restore "WebApi/WebApi.csproj"
RUN dotnet build "WebApi/WebApi.csproj" -c Release -o /app/build
RUN dotnet publish "WebApi/WebApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY WebApi/wait-for-mssql.sh .
RUN chmod +x wait-for-mssql.sh
RUN apt-get update && \
    apt-get install -y curl gnupg && \
    curl -sSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /etc/apt/trusted.gpg.d/microsoft.gpg && \
    curl -sSL https://packages.microsoft.com/config/debian/11/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql18 mssql-tools unixodbc-dev && \
    ln -sfn /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd && \
    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
ENTRYPOINT ["./wait-for-mssql.sh", "dotnet", "WebApi.dll"]

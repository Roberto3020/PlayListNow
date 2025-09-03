# Dockerfile para .NET WebApi
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7173

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

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]

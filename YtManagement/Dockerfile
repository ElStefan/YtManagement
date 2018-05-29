FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY YtManagement/YtManagement.csproj YtManagement/
RUN dotnet restore YtManagement/YtManagement.csproj
COPY . .
WORKDIR /src/YtManagement
RUN dotnet build YtManagement.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish YtManagement.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "YtManagement.dll"]

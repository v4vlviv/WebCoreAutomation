FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY WebCoreAutoTests/WebCoreAutoTests.csproj WebCoreAutoTests/
RUN dotnet restore WebCoreAutoTests/WebCoreAutoTests.csproj
COPY . .
WORKDIR /src/WebCoreAutoTests
RUN dotnet build WebCoreAutoTests.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebCoreAutoTests.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebCoreAutoTests.dll"]

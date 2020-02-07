FROM dysnomia/netcore-sdk-3-0 AS build-env
WORKDIR /app

# Build Project
COPY . ./
RUN dotnet sonarscanner begin /k:"whoowns-me" /d:sonar.host.url="***REMOVED***" /d:sonar.login="***REMOVED***" /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml" /d:sonar.coverage.exclusions="**Test*.cs"
RUN dotnet restore Dysnomia.WhoOwnsMe.sln --ignore-failed-sources /p:EnableDefaultItems=false
RUN dotnet publish Dysnomia.WhoOwnsMe.sln --no-restore -c Release -o out
RUN dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
RUN dotnet sonarscanner end /d:sonar.login="***REMOVED***"

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Dysnomia.WhoOwnsMe.WebApp.dll"]
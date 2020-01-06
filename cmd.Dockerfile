FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /OntoSemStatsLib
COPY /OntoSemStatsLib/. ./
WORKDIR /OntoSemStatsCmd
COPY /OntoSemStatsCmd/. ./
RUN dotnet build
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 as runtime
WORKDIR /app
COPY --from=build /OntoSemStatsCmd/out ./
ENTRYPOINT ["dotnet", "OntoSemStatsCmd.dll"]

# BUILD:
# docker build -f cmd.Dockerfile -t semstatscmd .
# RUN:
# For volumes according to operating system: https://stackoverflow.com/a/41489151
# docker run -v ${PWD}:/data --rm -it semstatscmd -e http://dbpedia.org/sparql -o /data/semstat_dbpedia.ttl -f ttl
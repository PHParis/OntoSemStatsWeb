FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /OntoSemStatsLib
COPY /OntoSemStatsLib/. ./
WORKDIR /OntoSemStatsWeb
COPY /OntoSemStatsWeb/. ./
RUN dotnet build
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime
RUN apt-get update -y && apt-get install graphviz -y
WORKDIR /app
COPY --from=build /OntoSemStatsWeb/out ./
ENTRYPOINT ["dotnet", "OntoSemStatsWeb.dll"]

# BUILD:
# docker build -f web.Dockerfile -t semstatsweb .
# RUN:
# For volumes according to operating system: https://stackoverflow.com/a/41489151
# docker run -it --rm -p 5000:80 -v ${PWD}:/data semstatsweb
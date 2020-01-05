FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /OntoSemStatsLib
COPY /OntoSemStatsLib/. ./
WORKDIR /OntoSemStatsWeb
COPY /OntoSemStatsWeb/. ./
RUN dotnet build
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime
RUN apt-get update -y && apt-get install graphviz -y
# RUN apk add --update --no-cache \
#     graphviz \
#     ttf-freefont ttf-ubuntu-font-family
WORKDIR /app
COPY --from=build /OntoSemStatsWeb/out ./
ENTRYPOINT ["dotnet", "OntoSemStatsWeb.dll"]
# BUILD:
# docker build -f web.Dockerfile -t semstatsweb .
# docker run -p 8000:80 -v ${PWD}:/data -it --rm semstatsweb

# Pour les volumes selon le systeme d'exploitation : https://stackoverflow.com/a/41489151
# docker run -v ${PWD}:/data --rm -it semstatscmd -e http://dbpedia.org/sparql -o /data/semstat_dbpedia.ttl -f ttl
# docker run -it --rm -p 8000:80 --name aspnetcore_sample mcr.microsoft.com/dotnet/core/samples:aspnetapp

# BUILD:
# docker build -f cmd.Dockerfile -t semstatsweb.
# docker run --name test --rm -it semstatsweb
# docker run -it --rm -p 8000:80 --name aspnetcore_sample mcr.microsoft.com/dotnet/core/samples:aspnetapp

# CMD ["/bin/echo", "Available commands: OntoSemStatsCmd.dll OntoSemStatsWeb.dll"]

# faire relase des apps

# ensuite environnement mcr.microsoft.com/dotnet/core/runtime:3.1

# #PlantUml
# WORKDIR /PlantUml.Net
# # Copy csproj and restore as distinct layers
# COPY /PlantUml.Net/*.csproj ./
# RUN dotnet restore

# # Copy everything else and build
# COPY /PlantUml.Net/. ./

# # lib
# WORKDIR /LOD-CM-LIB
# # Copy csproj and restore as distinct layers
# COPY /LOD-CM-LIB/*.csproj ./
# RUN dotnet restore
# RUN dotnet restore
                                          
# # Copy everything else and build
# COPY /LOD-CM-LIB/. ./

# # web interface
# WORKDIR /web

# # Copy csproj and restore as distinct layers
# COPY /LOD-CM-WEB/*.csproj ./
# RUN dotnet restore

# # Copy everything else and build
# COPY /LOD-CM-WEB/. ./
# RUN dotnet publish -c Release -o out

# # Build runtime image
# FROM microsoft/dotnet:aspnetcore-runtime
# WORKDIR /app
# COPY --from=build-env /web/out .
# ENTRYPOINT ["dotnet", "LOD-CM.dll", "/data"]
# # Pour construire l'image:
# # docker build -t semstatsweb .
# # Pour lancer le container (ps: je te laisse voir pour les ports avec les ingé):
# # docker run  --rm -v /data2/hamdif/doctorants/ph/xp/lod_cm:/data -d -p 84:80 --name lod_cm_web lod_cm
# # docker run  --rm -v E:/download:/data -d -p 8080:80 --name lod_cm_web lod_cm
# # Pour stopper et supprimer le container
# # docker stop lod_cm_web
# # git pull && docker build -t lod_cm . && docker stop lod_cm_web && docker run  --rm -v /data2/hamdif/doctorants/ph/xp/lod_cm:/data -d -p 84:80 --name lod_cm_web lod_cm
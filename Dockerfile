# Build stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine as build

ENV DOTNET_CLI_TELEMETRY_OPTOUT true

COPY src /src
WORKDIR /src

RUN dotnet publish -c Release

# Run stage
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine as run

RUN apk update && apk upgrade --no-cache

ARG CONVERTER_PORT

ENV CONVERTER_PORT ${CONVERTER_PORT}

EXPOSE ${CONVERTER_PORT}/tcp
ENV ASPNETCORE_URLS http://*:${CONVERTER_PORT}

COPY --from=build /src/bin/Release/netcoreapp3.0/publish /app
WORKDIR /app

# don't run as root user
RUN chown 1001:0 /app/Cdc.Nndss.Fhir.Web.dll
RUN chmod g+rwx /app/Cdc.Nndss.Fhir.Web.dll
USER 1001

ENTRYPOINT [ "dotnet", "Cdc.Nndss.Fhir.Web.dll" ]


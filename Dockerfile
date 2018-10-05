FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
LABEL maintainer="xitaocrazy"
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1.403-sdk-alpine3.7 AS build
WORKDIR /src
COPY . .
RUN dotnet restore \
    && dotnet build -c Release -o /app

FROM build AS publish
WORKDIR /src
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CalculadorDeJurosApi.dll"] 
FROM microsoft/dotnet:2.1.402-sdk AS build
LABEL maintainer="xitaocrazy"
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY CalculadorDeJurosApi/*.csproj ./CalculadorDeJurosApi/
COPY CalculadorDeJurosApiTests/*.csproj ./CalculadorDeJurosApiTests/
COPY CalculadorDeJurosApiIntegrationTests/*.csproj ./CalculadorDeJurosApiIntegrationTests/
RUN dotnet restore

# copy everything else and build app
COPY CalculadorDeJurosApi/. ./CalculadorDeJurosApi/
WORKDIR /app/CalculadorDeJurosApi
RUN dotnet publish -c Release -o out

# Runtime container
FROM microsoft/dotnet:2.1-runtime
WORKDIR /app
COPY --from=build /app/CalculadorDeJurosApi/out .
ENTRYPOINT ["dotnet", "CalculadorDeJurosApi.dll"]
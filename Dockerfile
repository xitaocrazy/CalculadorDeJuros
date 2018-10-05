FROM microsoft/dotnet:2.1.403-sdk AS build
LABEL maintainer="xitaocrazy"
WORKDIR /

# Copia todos os arquivos e faz o restore
COPY / ./
RUN dotnet restore

# Faz o release da API
COPY CalculadorDeJurosApi/*.csproj ./CalculadorDeJurosApi/
WORKDIR /CalculadorDeJurosApi
RUN dotnet publish -c Release -o out

# Runtime container
FROM microsoft/dotnet:2.1-runtime
WORKDIR /
COPY --from=build /CalculadorDeJurosApi/out .
ENTRYPOINT ["dotnet", "CalculadorDeJurosApi.dll"]
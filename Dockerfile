FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

RUN dotnet tool install --global dotnet-ef --version 6.0.12
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /app
EXPOSE 80

COPY Desafio.Api/*.csproj Desafio.Api/
COPY Desafio.Domain/*.csproj Desafio.Domain/
COPY Desafio.Service/*.csproj Desafio.Service/
COPY Desafio.Infra/*.csproj Desafio.Infra/
RUN dotnet restore Desafio.Api/Desafio.Api.csproj
COPY . .
RUN dotnet build Desafio.Api/Desafio.Api.csproj -c Release -o /app/build
RUN dotnet publish Desafio.Api/Desafio.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Desafio.Api.dll"]

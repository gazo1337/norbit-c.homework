FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build

WORKDIR /app

COPY *.sln .

COPY TaskProject/*.csproj ./TaskProject/
COPY TaskDomain/*.csproj ./TaskDomain/
COPY TaskInfrastructure/*.csproj ./TaskInfrastructure/

RUN dotnet restore -r alpine-x64

COPY TaskProject/. ./TaskProject/
COPY TaskDomain/. ./TaskDomain/
COPY TaskInfrastructure/. ./TaskInfrastructure/

RUN dotnet build

RUN dotnet publish -c Release -o /out -r alpine-x64

FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine AS publish

WORKDIR /app
COPY --from=build /out .

EXPOSE 80
ENTRYPOINT ["./TaskProject"]
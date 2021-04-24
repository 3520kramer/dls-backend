FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY SkoleProtokolAPI/*.csproj ./SkoleProtokolAPI/
COPY SkoleProtokolLibrary/*.csproj ./SkoleProtokolLibrary/

RUN dotnet restore

# copy everything else and build app
COPY SkoleProtokolAPI/. ./SkoleProtokolAPI/
COPY SkoleProtokolLibrary/. ./SkoleProtokolLibrary/

WORKDIR /app/SkoleProtokolAPI
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

# Setting environmental variables
ENV ASPNETCORE_ENVIRONMENT=”Development”
ENV SkoleProtokolMongoConnection=<INSERT CONNECTIONSTRING>
ENV FrontendURL="http://localhost:3000"

COPY --from=build /app/SkoleProtokolAPI/out ./
ENTRYPOINT ["dotnet", "SkoleProtokolAPI.dll"]

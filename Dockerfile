FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY BoxBattleServer/*.csproj ./BoxBattleServer/
RUN dotnet restore

# copy everything else and build app
COPY BoxBattleServer/. ./BoxBattleServer/
WORKDIR /app/BoxBattleServer
RUN dotnet publish -c Debug -o out

# run application
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/BoxBattleServer/server.pfx ./
COPY --from=build /app/BoxBattleServer/out ./
ENTRYPOINT ["dotnet", "BoxBattleServer.dll"]
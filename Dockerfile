FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY ./ ./

RUN --mount=type=secret,id=ghUser \
    --mount=type=secret,id=ghPass \
    export GITHUB_USERNAME=$(cat /run/secrets/ghUser) && \
    export GITHUB_PACKAGES_FEED_PAT=$(cat /run/secrets/ghPass) && \
	dotnet restore "SKB.CleanWebApi.sln"

COPY . .
RUN echo "# Sample gitignore for anchor" >> .gitignore
RUN --mount=type=secret,id=ghUser,env=GITHUB_USERNAME \
    --mount=type=secret,id=ghPass,env=GITHUB_PACKAGES_FEED_PAT \
	dotnet build "SKB.CleanWebApi.sln" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN --mount=type=secret,id=ghUser,env=GITHUB_USERNAME \
    --mount=type=secret,id=ghPass,env=GITHUB_PACKAGES_FEED_PAT \
	dotnet publish "SKB.CleanWebApi.sln" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SKB.App.CleanWebApi.Http.dll"]

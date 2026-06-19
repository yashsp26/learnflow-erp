# --- STAGE 1: Build & Publish ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything from the root directory context (excluding what's in .dockerignore)
COPY . ./

# Restore packages targeting the exact API project path
RUN dotnet restore "backend/LearnFlowERP.Api/LearnFlowERP.Api.csproj"

# Build and publish into the /app/out directory
RUN dotnet publish "backend/LearnFlowERP.Api/LearnFlowERP.Api.csproj" -c Release -o out

# --- STAGE 2: Runtime Execution ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "LearnFlowERP.Api.dll"]
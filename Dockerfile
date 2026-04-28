# =======================
# Stage 1: Runtime base
# =======================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production


# =======================
# Stage 2: Build
# =======================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy solution + projects first (for proper restore caching)
COPY ["ICIT.FacultyPortalSystem.sln", "./"]

COPY ["Core/Domain/Domain.csproj", "Core/Domain/"]
COPY ["Core/Services.Abstraction/Services.Abstraction.csproj", "Core/Services.Abstraction/"]
COPY ["FtpFileStorage/FtpFileStorage.csproj", "FtpFileStorage/"]
COPY ["ICT.FacultyPortalSystem.API/ICIT.FacultyPortalSystem.API.csproj", "ICT.FacultyPortalSystem.API/"]
COPY ["Infrastructure/Logging/Logging.csproj", "Infrastructure/Logging/"]
COPY ["Infrastructure/Messaging/Messaging.csproj", "Infrastructure/Messaging/"]
COPY ["Infrastructure/Presentation/Presentation.csproj", "Infrastructure/Presentation/"]
COPY ["Infrastructure/Presistence/Presistence.csproj", "Infrastructure/Presistence/"]
COPY ["Integrations/Integrations.csproj", "Integrations/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Shared/Shared.csproj", "Shared/"]

# Restore
RUN dotnet restore "ICIT.FacultyPortalSystem.sln"

# Copy full source code AFTER restore
COPY . .

# تثبيت EF Tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Build (لازم قبل EF)
RUN dotnet build "ICT.FacultyPortalSystem.API/ICIT.FacultyPortalSystem.API.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build \
    --no-restore

# =======================
# Stage 3: Publish
# =======================
FROM build AS publish

RUN dotnet publish "ICT.FacultyPortalSystem.API/ICIT.FacultyPortalSystem.API.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false 


# =======================
# Stage 4: Final
# =======================
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

COPY --from=build /src/ICT.FacultyPortalSystem.API/Fonts ./fonts

ENTRYPOINT ["dotnet", "ICIT.FacultyPortalSystem.API.dll"]
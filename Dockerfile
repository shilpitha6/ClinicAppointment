# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Install Node.js (required for SPA project reference)
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /src

# Cache npm dependencies
COPY clinicappointment.client/package*.json clinicappointment.client/
RUN cd clinicappointment.client && npm ci

# Restore .NET packages
COPY ClinicAppointment.Server/ClinicAppointment.Server.csproj ClinicAppointment.Server/
RUN dotnet restore "ClinicAppointment.Server/ClinicAppointment.Server.csproj"

# Copy all source and publish
COPY . .
RUN dotnet publish "ClinicAppointment.Server/ClinicAppointment.Server.csproj" \
    -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ClinicAppointment.Server.dll"]

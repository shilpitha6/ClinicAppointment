# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ClinicAppointment.Server/ClinicAppointment.Server.csproj ClinicAppointment.Server/
RUN dotnet restore "ClinicAppointment.Server/ClinicAppointment.Server.csproj" /p:DOCKER_BUILD=true

COPY ClinicAppointment.Server/ ClinicAppointment.Server/
RUN dotnet publish "ClinicAppointment.Server/ClinicAppointment.Server.csproj" \
    -c Release -o /app/publish --no-restore /p:DOCKER_BUILD=true

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ClinicAppointment.Server.dll"]

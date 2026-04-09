using ClinicAppointment.Server.DTO;

namespace ClinicAppointment.Server.Services.JwtService
{
    public interface IJwtService
    {
        Task<LoginResponseDTO> GenerateTokenAsync(LoginDTO dto);
    }
}

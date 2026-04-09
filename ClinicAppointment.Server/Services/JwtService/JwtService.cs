using ClinicAppointment.Server.DTO;
using ClinicAppointmentProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ClinicAppointment.Server.Services.JwtService
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        private readonly ClinicAppointmentDatabaseContext _context;

        public JwtService(IConfiguration config, ClinicAppointmentDatabaseContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<LoginResponseDTO> GenerateTokenAsync(LoginDTO dto)
        {
            var patient = await _context.Patient
                .FirstOrDefaultAsync(p => p.username == dto.username
                                       && p.password == dto.password);

            if (patient == null)
                throw new Exception("Invalid username or password.");

            var secretKey = _config["JwtSettings:SecretKey"]!;
            var issuer = _config["JwtSettings:Issuer"]!;
            var audience = _config["JwtSettings:Audience"]!;
            var expiryHrs = int.Parse(_config["JwtSettings:ExpiryInHours"]!);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,   patient.patient_id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, patient.email ?? ""),
                new Claim("patient_id",                  patient.patient_id.ToString()),
                new Claim("username",                    patient.username ?? ""),
                new Claim("first_name",                  patient.first_name ?? ""),
                new Claim("last_name",                   patient.last_name ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expiryHrs),
                signingCredentials: creds
            );

            return new LoginResponseDTO
            {
                patient_id = patient.patient_id,
                first_name = patient.first_name,
                last_name = patient.last_name,
                email = patient.email,
                username = patient.username,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires_at = DateTime.UtcNow.AddHours(expiryHrs)
            };
        }
    }
}

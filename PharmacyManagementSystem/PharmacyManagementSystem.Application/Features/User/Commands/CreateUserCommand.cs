using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Helpers;
using PharmacyManagementSystem.Application.Interfaces;
using PharmacyManagementSystem.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharmacyManagementSystem.Application.Features.User.Commands
{
    public record CreateUserCommand(CreateUserDto createUserDto) : IRequest<AuthResponseDto>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly JWT _jwtSettings;

        public CreateUserCommandHandler(IUserRepository userRepository, IOptions<JWT> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByNameAsync(request.createUserDto.UserName);
            if (existingUser != null)
            {
                throw new Exception($"A user with this name : {request.createUserDto.UserName} already exists.");
            }

            var existingEmail = await _userRepository.GetUserByEmailAsync(request.createUserDto.Email);
            if (existingEmail != null)
            {
                throw new Exception($"A user with this email : {request.createUserDto.Email} already exists.");
            }

            var hashedPassword = HashPassword(request.createUserDto.Password);

            var user = new Domain.Entities.User
            {
                Email = request.createUserDto.Email,
                FullName = request.createUserDto.FullName,
                PhoneNumber = request.createUserDto.PhoneNumber,
                UserName = request.createUserDto.UserName,
                UserRole = request.createUserDto.UserName.Contains("admin") ? UserRoles.Admin : UserRoles.Cashier,
                PasswordHash = hashedPassword
            };

            await _userRepository.CreateUserAsync(user)!;

            var token = GenerateToken(user!);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresIn = _jwtSettings.ExpirationInMinutes * 60,
                UserId = user!.UserId,
                UserName = user!.UserName,
                FullName = user!.FullName,
                Email = user!.Email,
                Role = user!.UserRole.ToString(),
                Password = user!.PasswordHash,
                IsAuthenticated = true
            };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private string GenerateToken(Domain.Entities.User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

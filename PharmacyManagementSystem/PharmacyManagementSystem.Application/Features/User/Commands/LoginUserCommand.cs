using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.Application.Helpers;
using PharmacyManagementSystem.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharmacyManagementSystem.Application.Features.User.Commands
{
    public record LoginUserCommand(LoginRequestDto loginRequestDto) : IRequest<AuthResponseDto>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly JWT _jwtSettings;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IOptions<JWT> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByNameAsync(request.loginRequestDto.UserName);
            if (existingUser == null)
            {
                throw new Exception("Incorrect username or password.");
            }

            if (existingUser.UserName != request.loginRequestDto.UserName)
            {
                throw new Exception("Incorrect username or password.");
            }

            if (existingUser.Email != request.loginRequestDto.Email)
            {
                throw new Exception("Incorrect username or password.");
            }

            if (!VerifyPassword(request.loginRequestDto.Password, existingUser.PasswordHash))
            {
                throw new Exception("Incorrect username or password.");
            }

            var token = GenerateToken(existingUser);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresIn = _jwtSettings.ExpirationInMinutes * 60,
                UserId = existingUser.UserId,
                UserName = existingUser.UserName,
                FullName = existingUser.FullName,
                Email = existingUser.Email,
                Role = existingUser.UserRole.ToString(),
                Password = existingUser.PasswordHash,
                IsAuthenticated = true
            };
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


        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
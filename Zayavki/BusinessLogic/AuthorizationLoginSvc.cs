using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Zayavki;

public class AuthorizationLoginSvc : IAuthorizationLoginSvc
{
    private readonly IAuthorizationRepository _authorizationRepository;
    private readonly IConfiguration _configuration;

    public AuthorizationLoginSvc(IAuthorizationRepository authorizationRepository, IConfiguration configuration)
    {
        _authorizationRepository = authorizationRepository;
        _configuration = configuration;


    }

    public async Task<LoginResponse?> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(request.Password);
        var encryptedPassword = Convert.ToBase64String(passwordBytes);
        var user = await _authorizationRepository.GetUser(request.UserName, encryptedPassword, cancellationToken);

        var token = GenerateAccessToken(request.UserName);

        return user == null ? 
        null : 
        new LoginResponse ( UserName : user.UserName, Role : user.Role, 
        Token: new JwtSecurityTokenHandler().WriteToken(token));
    }

private JwtSecurityToken GenerateAccessToken(string userName)
    {
        // Create user claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userName),
            // Add additional claims as needed (e.g., roles, etc.)
        };

        // Create a JWT
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60), // Token expiration time
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}

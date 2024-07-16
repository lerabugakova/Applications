using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Zayavki;

public class AuthorizationLoginSvc : IAuthorizationLoginsvc
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public AuthorizationLoginSvc(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<LoginResponse?> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var passwortBytes = Encoding.UTF8.GetBytes(request.Password);
        var encryptedPassword = System.Convert.ToBase64String(passwortBytes);
        var user = await _authorizationRepository.GetUser(request.UserName, encryptedPassword, cancellationToken);

        var token = GenerateAccessToken(request.Password);

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
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60), // Token expiration time
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkey")),
                SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}

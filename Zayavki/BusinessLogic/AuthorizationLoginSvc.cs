namespace Zayavki;

public class AuthorizationLoginSvc : IAuthorizationLoginsvc
{
    private readonly IAuthorizationRepository _authorizationRepository;
    private readonly ITokenService _tokenService;

    public AuthorizationLoginSvc(IAuthorizationRepository authorizationRepository, ITokenService tokenService)
    {
        _authorizationRepository = authorizationRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _authorizationRepository.GetUser(request.UserName, request.Password, cancellationToken);

        if (user == null)
        {
            return new LoginResponse
            {
                IsSuccess = false,
                Message = "Invalid username or password"
            };
        }

        var token = _tokenService.GenerateToken(user);

        return new LoginResponse
        {
            IsSuccess = true,
            Token = token
        };
    }
}

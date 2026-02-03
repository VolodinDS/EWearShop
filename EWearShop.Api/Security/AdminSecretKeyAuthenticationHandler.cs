using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace EWearShop.Api.Security;

internal sealed class AdminSecretKeyAuthenticationHandler : AuthenticationHandler<AdminSecretKeyOptions>
{
    private readonly IOptionsMonitor<AdminSecretKeyOptions> _options;
    private const string ApiKeyHeaderName = "X-Admin-Key";

    public AdminSecretKeyAuthenticationHandler(
        IOptionsMonitor<AdminSecretKeyOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
        _options = options;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out StringValues extractedApiKey))
        {
            return Task.FromResult(AuthenticateResult.Fail("API Key missing"));
        }

        if (_options.CurrentValue.AdminSecretKey != extractedApiKey)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API Key"));
        }

        Claim[] claims = [new(ClaimTypes.Name, "Admin")];
        ClaimsIdentity identity = new(claims, Scheme.Name);
        ClaimsPrincipal principal = new(identity);
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
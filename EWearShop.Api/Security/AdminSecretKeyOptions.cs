using Microsoft.AspNetCore.Authentication;

namespace EWearShop.Api.Security;

public sealed class AdminSecretKeyOptions : AuthenticationSchemeOptions
{
    public string AdminSecretKey { get; set; } = string.Empty;
}
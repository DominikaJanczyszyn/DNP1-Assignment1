using Microsoft.Extensions.DependencyInjection;

namespace Assignment1.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            ///some authorization;
        });
    }
}
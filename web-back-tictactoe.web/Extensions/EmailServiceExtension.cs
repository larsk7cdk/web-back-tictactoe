using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using web_back_tictactoe.web.Options;
using web_back_tictactoe.web.Services;

namespace web_back_tictactoe.web.Extensions
{
    //public static class EmailServiceExtension
    //{
    //    public static IServiceCollection AddEmailService(this IServiceCollection services,
    //        IHostingEnvironment hostingEnvironment, IConfiguration configuration)
    //    {
    //        services.Configure<EmailServiceOptions>(configuration.GetSection("Email"));
    //        if (hostingEnvironment.IsDevelopment() || hostingEnvironment.IsStaging())
    //            services.AddSingleton<IEmailService, EmailService>();
    //        else
    //            services.AddSingleton<IEmailService, SendGridEmailService>();
    //        return services;
    //    }
    //}
}
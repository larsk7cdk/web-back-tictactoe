using Microsoft.AspNetCore.Builder;
using TicTacToe.Middlewares;

namespace web_back_tictactoe.web.Extensions
{
    public static class CommunicationMiddlewareExtension
    {
        public static IApplicationBuilder UseCommunicationMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CommunicationMiddleware>();
        }
    }
}

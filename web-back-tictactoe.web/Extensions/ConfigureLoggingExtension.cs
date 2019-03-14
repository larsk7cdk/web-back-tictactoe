using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using web_back_tictactoe.logging;

namespace web_back_tictactoe.web.Extensions
{
    public static class ConfigureLoggingExtension
    {
        public static ILoggingBuilder AddLoggingConfiguration(this ILoggingBuilder loggingBuilder,
            IConfiguration configuration)
        {
            var loggingOptions = new Options.LoggingOptions();
            configuration.GetSection("Logging").Bind(loggingOptions);

            foreach (var provider in loggingOptions.Providers)
                switch (provider.Name.ToLower())
                {
                    case "console":
                    {
                        loggingBuilder.AddConsole();
                        break;
                    }
                    case "file":
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "logs",
                            $"TicTacToe_{DateTime.Now.ToString("ddMMyyHHmm")}.log");
                        loggingBuilder.AddFile(filePath, (LogLevel) provider.LogLevel);
                        break;
                    }
                }

            return loggingBuilder;
        }
    }
}
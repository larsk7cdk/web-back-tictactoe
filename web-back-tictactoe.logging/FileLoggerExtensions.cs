﻿using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace web_back_tictactoe.logging
{
    public static class FileLoggerExtensions
    {
        private const long DefaultFileSizeLimitBytes = 1024 * 1024 * 1024;
        private const int DefaultRetainedFileCountLimit = 31;

        public static ILoggingBuilder AddFile(this ILoggingBuilder loggerBuilder, IConfigurationSection configuration)
        {
            if (loggerBuilder == null) throw new ArgumentNullException(nameof(loggerBuilder));

            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var minimumLevel = LogLevel.Information;

            var levelSection = configuration["Logging:LogLevel"];

            if (!string.IsNullOrWhiteSpace(levelSection))
                if (!Enum.TryParse(levelSection, out minimumLevel))
                {
                    Debug.WriteLine("The minimum level setting `{0}` is invalid", levelSection);
                    minimumLevel = LogLevel.Information;
                }

            return loggerBuilder.AddFile(configuration["Logging:FilePath"],
                (category, logLevel) => logLevel >= minimumLevel, minimumLevel);
        }

        public static ILoggingBuilder AddFile(this ILoggingBuilder loggerBuilder, string filePath,
            Func<string, LogLevel, bool> filter, LogLevel minimumLevel = LogLevel.Information)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));

            var fileInfo = new FileInfo(filePath);

            if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            loggerBuilder.AddProvider(new FileLoggerProvider(filter, filePath));

            return loggerBuilder;
        }

        public static ILoggingBuilder AddFile(this ILoggingBuilder loggerBuilder, string filePath,
            LogLevel minimumLevel = LogLevel.Information)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));

            var fileInfo = new FileInfo(filePath);

            if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            loggerBuilder.AddProvider(
                new FileLoggerProvider((category, logLevel) => logLevel >= minimumLevel, filePath));

            return loggerBuilder;
        }
    }
}
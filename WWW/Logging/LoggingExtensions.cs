namespace WWW.Logging;

public static class LoggingExtensions
{
    public static ILoggingBuilder EnableLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        return builder.Logging;
    }
}
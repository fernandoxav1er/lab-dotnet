using System.Collections.Concurrent;

namespace Catalogo.API.LoggingCustom;

public class CustomLoggerProvider : ILoggerProvider
{
    private CustomLoggerProviderConfiguration _loggerConfig;
    private readonly ConcurrentDictionary<string, CustomerLogger> loggers =
        new ConcurrentDictionary<string, CustomerLogger>();
    public CustomLoggerProvider( CustomLoggerProviderConfiguration loggerConfig)
    {
        _loggerConfig = loggerConfig;
    }
    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, _loggerConfig));
    }

    public void Dispose()
    {
        loggers.Clear();
    }
}

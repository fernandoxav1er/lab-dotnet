
namespace Catalogo.API.LoggingCustom;

public class CustomerLogger : ILogger
{
    private readonly string _name;
    private readonly CustomLoggerProviderConfiguration _config;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        _name = name;
        _config = config;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == _config.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string msg = $"{logLevel.ToString()} : {eventId.Id} - {formatter(state,exception)}";
        EscreverTextoNoArquivo(msg);
    }

    private void EscreverTextoNoArquivo(string msg)
    {
        string caminhoDestino = @"C:\dev\logs\api-catalogo.txt";
        using(var streamWriter = new StreamWriter(caminhoDestino, true))
        {
            try
            {
                streamWriter.WriteLine(msg);
                streamWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

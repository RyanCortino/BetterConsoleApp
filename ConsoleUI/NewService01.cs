using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class NewService01 : INewService01
{
    private readonly ILogger<NewService01> _log;
    private readonly IConfiguration _config;

    public NewService01(ILogger<NewService01> log, IConfiguration config)
    {
        _log = log;
        _config = config;
    }

    public void Run()
    {

        // example of how to inject dependencies and configuration data
        for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
        {
            _log.LogInformation("Run number {}", i);
        }
    }
}

public interface INewService01
{
    void Run();
}
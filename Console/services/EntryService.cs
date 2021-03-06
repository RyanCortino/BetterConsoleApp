public class EntryService : IEntryService
{
    public void Run(string[] args)
        => MainAsync(args).GetAwaiter().GetResult();

    private async Task MainAsync(string[] args)
    {
        // block this task until the program is closed
        await Task.Delay(Timeout.Infinite);
    }
}
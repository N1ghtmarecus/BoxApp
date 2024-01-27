using BoxApp.Components.DataProviders;

namespace BoxApp;

public class AppCsv(ICsvProvider csvProvider) : IApp
{
    private readonly ICsvProvider _csvProvider = csvProvider;

    public void Run()
    {
        _csvProvider.CsvFile();
    }
}

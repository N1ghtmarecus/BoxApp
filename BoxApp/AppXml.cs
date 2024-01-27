using BoxApp.Components.CsvReader;

namespace BoxApp;

public class AppXml(IXmlCreator xmlCreator) : IApp
{
    private readonly IXmlCreator _xmlCreator = xmlCreator;

    public void Run()
    {
        _xmlCreator.CreateXml();
        _xmlCreator.QueryXml();
        _xmlCreator.CreateXmlGroupJoined();
    }
}

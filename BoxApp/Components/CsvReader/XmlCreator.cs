using System.Xml.Linq;

namespace BoxApp.Components.CsvReader;

internal class XmlCreator(ICsvReader csvReader) : IXmlCreator
{
    private readonly ICsvReader _csvReader = csvReader;

    public void CreateXml()
    {
        var recordCars = _csvReader.ProcessCars(@"Resources\Files\fuel.csv");

        var document = new XDocument();
        var cars = new XElement("Cars", recordCars
            .Select(x =>
                new XElement("Car",
                    new XAttribute("Name", x.Name),
                    new XAttribute("Combined", x.Combined),
                    new XAttribute("Manufacturer", x.Manufacturer)
                    )
            ));

        document.Add(cars);
        document.Save("fuel.xml");
        Console.WriteLine($"\nSaving file {"fuel.xml"} succesfull!\n");
    }

    public void QueryXml()
    {
        var document = XDocument.Load("fuel.xml");
        var names = document
            .Element("Cars")?
            .Elements("Car")
            .Where(x => x.Attribute("Manufacturer")?.Value == "BMW")
            .Select(x => x.Attribute("Name")?.Value);

        if (names != null)

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        Console.WriteLine($"\nLoading file {"fuel.xml"} succesfull!\n");
    }

    public void CreateXmlGroupJoined()
    {
        var recordCars = _csvReader.ProcessCars(@"Resources\Files\fuel.csv");
        var recordManufacturers = _csvReader.ProcessManufacturers(@"Resources\Files\manufacturers.csv");

        var groupJoined = recordManufacturers.GroupJoin(
            recordCars,
            manufacturer => manufacturer.Name,
            car => car.Manufacturer,
            (m, g) =>
            new
            {
                Manufacturer = m,
                Cars = g
            }

            )
            .OrderBy(x => x.Manufacturer.Name);

        var document = new XDocument();

        var manufacturers = new XElement("Manufacturers", groupJoined
        .Select(x =>
        new XElement("Manufacturer",
            new XAttribute("Name", x.Manufacturer.Name!),
            new XAttribute("Country", x.Manufacturer.Country!),
                new XElement("Cars",
                    new XAttribute("Country", x.Manufacturer.Country!),
                    new XAttribute("CombinedSum", x.Cars.Sum(c => c.Combined)),
                        new XElement("Car", x.Cars
                            .Select(c =>
                           new XElement("Car",
                               new XAttribute("Model", c.Name),
                               new XAttribute("Combined", c.Combined))))))));

        document.Add(manufacturers);
        document.Save("fuel2.xml");

        Console.WriteLine($"Saving file {"fuel2.xml"} succesfull!");
    }
}

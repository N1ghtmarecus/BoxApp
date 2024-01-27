using BoxApp.Components.CsvReader.Models;

namespace BoxApp.Components.CsvReader;

public interface ICsvReader
{
    List<Car> ProcessCars(string filePath);

    List<Manufacturer> ProcessManufacturers(string filePath);
}

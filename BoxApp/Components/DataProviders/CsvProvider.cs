using BoxApp.Components.CsvReader;
using BoxApp.Components.CsvReader.Models;

namespace BoxApp.Components.DataProviders
{
    public class CsvProvider(ICsvReader csvReader) : ICsvProvider
    {
        private readonly ICsvReader _csvReader = csvReader;

        public void CsvFile()
        {
            var cars = _csvReader.ProcessCars(@"Resources\Files\fuel.csv");
            var manufacturers = _csvReader.ProcessManufacturers(@"Resources\Files\manufacturers.csv");

            // Advanced LINQ Methods
            GroupByManufacturers(cars);
            JoinCarsAndManufacturers(cars, manufacturers);
            GroupJoinCarsAndManufacturersByManufacturer(cars, manufacturers);
        }

        private static void GroupByManufacturers(List<Car> cars)
        {
            var groups = cars
                .GroupBy(car => car.Manufacturer)
                .Select(g => new
                {
                    Name = g.Key,
                    Max = g.Max(car => car.Combined),
                    Average = g.Average(car => car.Combined)
                })
                .OrderBy(car => car.Average);

            foreach (var group in groups)
            {
                Console.WriteLine($"\n{group.Name}");
                Console.WriteLine($"\t Max: {group.Max}");
                Console.WriteLine($"\t Avg: {group.Average}");
            }
        }

        private static void JoinCarsAndManufacturers(List<Car> cars, List<Manufacturer> manufacturers)
        {
            var carsInCountry = cars.Join(
                manufacturers,
                x => x.Manufacturer,
                x => x.Name,
                (car, manufacturer) =>
                    new
                    {
                        manufacturer.Country,
                        car.Name,
                        car.Combined
                    })
                .OrderByDescending(x => x.Combined)
                .ThenBy(x => x.Name);

            foreach (var car in carsInCountry)
            {
                Console.WriteLine($"\n Country: {car.Country}");
                Console.WriteLine($"\t Name: {car.Name}");
                Console.WriteLine($"\t Combined: {car.Combined}");
            }
        }

        private static void GroupJoinCarsAndManufacturersByManufacturer(List<Car> cars, List<Manufacturer> manufacturers)
        {
            var groups = manufacturers.GroupJoin(
                cars,
                manufacturer => manufacturer.Name,
                car => car.Manufacturer,
                (m, g) =>
                new
                {
                    Manufacturer = m,
                    Cars = g
                })
                .OrderBy(x => x.Manufacturer.Name);

            foreach (var group in groups)
            {
                Console.WriteLine($"\n Manufacturer: {group.Manufacturer.Name}");
                Console.WriteLine($"\t Cars: {group.Cars.Count()}");
                Console.WriteLine($"\t Max: {group.Cars.Max(x => x.Combined)}");
                Console.WriteLine($"\t Min: {group.Cars.Min(x => x.Combined)}");
                Console.WriteLine($"\t Avg: {group.Cars.Average(x => x.Combined)}");
            }
        }
    }
}
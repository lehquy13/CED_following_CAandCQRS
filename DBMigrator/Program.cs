// See https://aka.ms/new-console-template for more information

using CED.Domain;
using CED.Domain.Users;
using CED.Infrastructure.Entity_Framework_Core;
using MapsterMapper;

namespace DBMigrator;

internal static class Program
{
    public static void Main(string[] args)
    {
        var mapper = new Mapper();
        var factory = new CEDDBContextFactory();
        var context = factory.CreateDbContext(args);

        context.Database.EnsureCreated();
       
        if (!context.Subjects.Any())
        {
            var seeder = new DataSeeder();
            context.Subjects.AddRange(seeder.Subjects);
            context.Users.AddRange(seeder.Users);
            context.ClassInformations.AddRange(seeder.ClassInformations);
            var addresses = ConvertCSVtoDataTable(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\")) +
                                                  "AddressData.csv");

            var cities = mapper
                .Map<List<City>>(from address in addresses
                    group address by new { address.City, address.CityId }
                    into city
                    select new
                    {
                        Name = city.Key.City,
                        Id = city.Key.CityId
                    });

            var districts = mapper
                .Map<List<District>>(from address in addresses
                    group address by new { address.District, address.DistrictId, address.CityId }
                    into city
                    select new
                    {
                        Name = city.Key.District,
                        Id = city.Key.DistrictId,
                        CityId = city.Key.CityId
                    });

            var wards = mapper
                .Map<List<Ward>>(
                    from address in addresses
                    where address.Ward != ""
                    select new
                    {
                        Name = address.Ward ?? address.District,
                        Id = address.WardId??address.DistrictId,
                        DistrictId = address.DistrictId,
                        WardLevel = address.WardLevel,
                        EnglishName = address.EnglishName ?? "",
                    }
                );

            context.Cities.AddRange(cities);
            context.Districts.AddRange(districts);
            context.Wards.AddRange(wards);
            context.SaveChanges();
            context.SaveChanges();
        }
    }

    private static List<Address> ConvertCSVtoDataTable(string strFilePath)
    {
        List<Address> dt = new List<Address>();
        using StreamReader sr = new StreamReader(strFilePath);
        string[] headers = sr.ReadLine()?.Split(',') ?? throw new InvalidOperationException();


        while (!sr.EndOfStream)
        {
            string[] rows = sr.ReadLine().Split(',');
            if (rows.Length == 8)
            {
                dt.Add(new()
                {
                    City = rows[0],
                    CityId = rows[1],
                    District = rows[2],
                    DistrictId = rows[3],
                    Ward = rows[4],
                    WardId = rows[5],
                    WardLevel = rows[6],
                    EnglishName = rows[7]
                });
            }
        }

        return dt;
    }
}
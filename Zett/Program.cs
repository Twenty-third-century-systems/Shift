using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Fridge.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Zett.World;

namespace Zett {
    class Program {
        static void Main(string[] args)
        {
            MainDatabaseContext mainDatabase = new MainDatabaseContext();
            shwaContext worldDatabase = new shwaContext();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Country, Fridge.Models.Country>();
                cfg.CreateMap<City, Fridge.Models.City>();
            });

            var mapper = new Mapper(mapperConfiguration);
            var countries =
                mapper.Map<List<Country>, List<Fridge.Models.Country>>(worldDatabase.Countries.Include(c => c.Cities)
                    .ToList());
            mainDatabase.Countries.AddRange(countries);
            mainDatabase.SaveChanges();
        }
    }
}
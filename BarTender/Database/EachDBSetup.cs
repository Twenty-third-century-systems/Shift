﻿using System.Collections.Generic;
using System.Linq;
using LinqToDB.Configuration;

namespace BarTender.Database
{
    public class EachDbSetup : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";
        
        public IEnumerable<IConnectionStringSettings> ConnectionStrings {
            get { 
                yield return 
                    new ConnectionStringSettings
                    {
                        Name = "SqlServer",
                        ProviderName = "SqlServer",
                        ConnectionString = @"Server=localhost;Database=each;Trusted_Connection=True;Enlist=False;"
                    };
                
            }
        }
    }
}
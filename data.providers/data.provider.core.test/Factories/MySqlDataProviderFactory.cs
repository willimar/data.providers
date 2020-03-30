using data.provider.core.mysql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace data.provider.core.test.Factories
{
    public class UsingMySql : DbContext
    {
        private static DbContextOptions Factory()
        {
            const string CONNECTIONSTRING = @"Server={0}{4};Database={1};Uid={2};Pwd={3};";
            var builder = new DbContextOptionsBuilder();
            var connectionString = string.Format(CONNECTIONSTRING, "localhost", "appTest", "root", "!sql2020", 3306 > 0 ? $";Port=3306" : string.Empty);
            builder.UseMySql(connectionString);

            return builder.Options;
        }

        public UsingMySql(DbContextOptions options) : base(options)
        {

        }

        public UsingMySql() : base(Factory())
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TesteConfig().Configure(modelBuilder.Entity<Teste>());
        }
    }

    internal class MySqlDataProviderFactory
    {
        public static IDataProvider Factory()
        {
            const string CONNECTIONSTRING = @"Server={0}{4};Database={1};Uid={2};Pwd={3};";
            var builder = new DbContextOptionsBuilder();
            var connectionString = string.Format(CONNECTIONSTRING, "localhost", "appTest", "root", "!sql2020", 3306 > 0 ? $";Port=3306" : string.Empty);
            builder.UseMySql(connectionString);

            var context = new UsingMySql(builder.Options);

            return new DataProvider(context);
        }
    }
}

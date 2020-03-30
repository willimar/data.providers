using data.provider.core.sqlserver;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace data.provider.core.test.Factories
{
    public class UsingSqlServer : DbContext
    {
        public static DbContextOptions Factory()
        {
            const string CONNECTIONSTRING = @"Data Source={0}{4};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
            var builder = new DbContextOptionsBuilder();
            var connectionString = string.Format(CONNECTIONSTRING, "localhost", "appTest", "sa", "!sql2020", 0 > 0 ? 0.ToString(",0") : string.Empty);
            builder.UseSqlServer(connectionString);
            return builder.Options;
        }

        public UsingSqlServer(DbContextOptions options) : base(options)
        {

        }

        public UsingSqlServer() : base(Factory())
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TesteConfig().Configure(modelBuilder.Entity<Teste>());
        }
    }

    public class SqlServerDataProviderFactory
    {
        public static IDataProvider Factory()
        {
            const string CONNECTIONSTRING = @"Data Source={0}{4};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
            var builder = new DbContextOptionsBuilder();
            var connectionString = string.Format(CONNECTIONSTRING, "localhost", "appTest", "sa", "!sql2020", 0 > 0 ? 0.ToString(",0") : string.Empty);
            builder.UseSqlServer(connectionString);
            var context = new UsingSqlServer(builder.Options);

            return new DataProvider(context);
        }
    }
}

using data.provider.core.test.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace data.provider.core.test
{
    public class MySqlTest
    {
        [Fact]
        public void InsertData()
        {
            var teste = Teste.Factory();
            using (var provider = MySqlDataProviderFactory.Factory())
            {
                using (var dataset = provider.GetDataSet<Teste>())
                {
                    dataset.Append(new List<Teste>() { teste });
                }
            }
        }

        [Fact]
        public void DeleteData()
        {
            var teste = Teste.Factory();
            using (var provider = MySqlDataProviderFactory.Factory())
            {
                using (var dataset = provider.GetDataSet<Teste>())
                {
                    dataset.Append(new List<Teste>() { teste });
                    var result = dataset.DeleteRecords(t => t.Id == teste.Id);

                    Assert.Equal(1, result);
                }
            }
        }

        [Fact]
        public void UpdateData()
        {
            var teste = Teste.Factory();

            using (var provider = MySqlDataProviderFactory.Factory())
            {
                using (var dataset = provider.GetDataSet<Teste>())
                {
                    dataset.Append(new List<Teste>() { teste });

                    teste.Name = Guid.NewGuid().ToString();

                    var result = dataset.UpdateRecords(t => t.Id == teste.Id, teste);

                    Assert.Equal(1, result);
                }
            }
        }

        [Fact]
        public void CountData()
        {
            var teste = Teste.Factory();

            using (var provider = MySqlDataProviderFactory.Factory())
            {
                using (var dataset = provider.GetDataSet<Teste>())
                {
                    dataset.Append(new List<Teste>() { teste });

                    teste.Name = Guid.NewGuid().ToString();

                    var result = dataset.Count(t => t.Id == teste.Id);

                    Assert.Equal(1, result);
                }
            }
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        public void GetData(int limit)
        {
            var teste = Teste.Factory();

            using (var provider = MySqlDataProviderFactory.Factory())
            {
                using (var dataset = provider.GetDataSet<Teste>())
                {
                    dataset.Append(new List<Teste>() { teste });

                    teste.Name = "Update record";

                    var result = dataset.UpdateRecords(t => t.Id == teste.Id, teste);
                    var records = dataset.GetEntities(e => e.Name == teste.Name, limit);

                    Assert.Equal(1, result);
                    Assert.Equal(limit, records.Count());
                }
            }
        }

        [Fact]
        public void GetRecord()
        {
            var teste = Teste.Factory();

            using (var provider = MySqlDataProviderFactory.Factory())
            {
                using (var dataset = provider.GetDataSet<Teste>())
                {
                    dataset.Append(new List<Teste>() { teste });

                    teste.Name = "Update record";

                    var records = dataset.GetEntities(t => t.Id == teste.Id);

                    Assert.Single(records);
                }
            }
        }
    }
}

using data.provider.core.mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace data.provider.core.test.Factories
{
    internal class Teste
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public Guid Id { get; set; }

        public static Teste Factory()
        {
            var id = Guid.NewGuid();
            var date = DateTime.UtcNow;

            return new Teste()
            {
                DateTime = date,
                Id = id,
                Name = $"Teste Provider Guid {id} and date {date}"
            };
        }
    }
}

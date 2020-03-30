using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace data.provider.core.test.Factories
{
    internal class TesteConfig
    {
        protected const string VARCHAR = "varchar";
        protected const string UNIQUEIDENTIFIER = "UNIQUEIDENTIFIER";
        protected const string DATETIME = "DATETIME";

        public virtual void Configure(EntityTypeBuilder<Teste> builder)
        {
            builder.HasKey("Id");
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType($"{VARCHAR}(150)")
                .HasMaxLength(150);
            builder.Property(x => x.DateTime)
                .IsRequired()
                .HasColumnType(DATETIME);
        }
    }
}

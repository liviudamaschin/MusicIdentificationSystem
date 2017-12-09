using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Mappings
{
    public class ClientEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ClientEntity>
    {
        public ClientEntityMapping()
            : this("dbo")
        {
        }

        public ClientEntityMapping(string schema)
        {
            ToTable("Clients", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsOptional().HasMaxLength(50);
            Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar").IsOptional().HasMaxLength(50);
            Property(x => x.Address).HasColumnName(@"Address").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);
            Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsOptional();
        }
    }
}

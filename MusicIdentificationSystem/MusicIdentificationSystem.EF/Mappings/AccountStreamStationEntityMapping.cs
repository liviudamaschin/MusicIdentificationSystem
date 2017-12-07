using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Mappings
{
    public class AccountStreamStationEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AccountStreamStationEntity>
    {
        public AccountStreamStationEntityMapping()
            : this("dbo")
        {
        }

        public AccountStreamStationEntityMapping(string schema)
        {
            ToTable("AccountStreamStations", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.AccountId).HasColumnName(@"AccountId").HasColumnType("int").IsRequired();
            Property(x => x.StreamStationId).HasColumnName(@"StreamStationId").HasColumnType("int").IsRequired();
            Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            
        }
    }
}

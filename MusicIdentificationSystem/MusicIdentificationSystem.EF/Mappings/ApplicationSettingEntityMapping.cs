using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Mappings
{
    public class ApplicationSettingEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ApplicationSettingEntity>
    {
        public ApplicationSettingEntityMapping()
            : this("dbo")
        {
        }

        public ApplicationSettingEntityMapping(string schema)
        {
            ToTable("ApplicationSettings", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.KeyName).HasColumnName(@"KeyName").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);
            Property(x => x.KeyValue).HasColumnName(@"KeyValue").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);
        }
    }
}

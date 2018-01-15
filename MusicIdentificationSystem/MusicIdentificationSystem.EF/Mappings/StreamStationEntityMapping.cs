using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Mappings
{
    public class StreamStationEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<StreamStationEntity>
    {
        public StreamStationEntityMapping()
            : this("dbo")
        {
        }

        public StreamStationEntityMapping(string schema)
        {
            ToTable("StreamStations", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.StationName).HasColumnName(@"StationName").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);
            Property(x => x.Url).HasColumnName(@"URL").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.LocalPath).HasColumnName(@"LocalPath").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsOptional();
            Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);
            Property(x => x.TransformFolder).HasColumnName(@"TransformFolder").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);
        }
    }
}

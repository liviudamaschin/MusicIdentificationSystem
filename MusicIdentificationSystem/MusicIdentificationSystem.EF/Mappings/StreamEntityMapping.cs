using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Mappings
{
    public class StreamEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<StreamEntity>
    {
        public StreamEntityMapping()
            : this("dbo")
        {
        }

        public StreamEntityMapping(string schema)
        {
            ToTable("Stream", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.StationId).HasColumnName(@"StationId").HasColumnType("int").IsOptional();
            Property(x => x.StartTime).HasColumnName(@"StartTime").HasColumnType("datetime").IsOptional();
            Property(x => x.EndTime).HasColumnName(@"EndTime").HasColumnType("datetime").IsOptional();
            Property(x => x.FileName).HasColumnName(@"FileName").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);

            // Foreign keys
            HasOptional(a => a.StreamStation).WithMany(b => b.Streams).HasForeignKey(c => c.StationId).WillCascadeOnDelete(false); // FK_Stream_StreamStations
        }
    }
}

using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Mappings
{
    public class ResultEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ResultEntity>
    {
        public ResultEntityMapping()
            : this("dbo")
        {
        }

        public ResultEntityMapping(string schema)
        {
            ToTable("Results", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.StreamId).HasColumnName(@"StreamId").HasColumnType("int").IsOptional();
            Property(x => x.TrackId).HasColumnName(@"TrackId").HasColumnType("int").IsOptional();
            Property(x => x.Filename).HasColumnName(@"filename").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);
            Property(x => x.MatchStartAt).HasColumnName(@"MatchStartAt").HasColumnType("decimal").IsOptional().HasPrecision(32, 6);

            // Foreign keys
            HasOptional(a => a.Stream).WithMany(b => b.Results).HasForeignKey(c => c.StreamId).WillCascadeOnDelete(false); // FK_TrackResults_Stream
            HasOptional(a => a.Track).WithMany(b => b.Results).HasForeignKey(c => c.TrackId).WillCascadeOnDelete(false); // FK_TrackResults_Tracks
        }
    }
}

using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Mappings
{
    public class TrackEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TrackEntity>
    {
        public TrackEntityMapping()
            : this("dbo")
        {
        }

        public TrackEntityMapping(string schema)
        {
            ToTable("Tracks", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Isrc).HasColumnName(@"ISRC").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(50);
            Property(x => x.Artist).HasColumnName(@"Artist").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Title).HasColumnName(@"Title").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Album).HasColumnName(@"Album").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ReleaseYear).HasColumnName(@"ReleaseYear").HasColumnType("int").IsOptional();
            Property(x => x.Length).HasColumnName(@"Length").HasColumnType("float").IsOptional();
            Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsOptional();
        }
    }
}

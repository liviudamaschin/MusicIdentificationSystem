using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Mappings
{
    public class FingerprintEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<FingerprintEntity>
    {
        public FingerprintEntityMapping()
            : this("dbo")
        {
        }

        public FingerprintEntityMapping(string schema)
        {
            ToTable("Fingerprints", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Signature).HasColumnName(@"Signature").HasColumnType("varbinary").IsRequired().HasMaxLength(4096);
            Property(x => x.TrackId).HasColumnName(@"TrackId").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.Track).WithMany(b => b.Fingerprints).HasForeignKey(c => c.TrackId).WillCascadeOnDelete(false); // FK_Fingerprints_Tracks
        }
    }
}

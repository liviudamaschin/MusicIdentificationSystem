using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Mappings
{
    public class SubFingerprintEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<SubFingerprintEntity>
    {
        public SubFingerprintEntityMapping()
            : this("dbo")
        {
        }

        public SubFingerprintEntityMapping(string schema)
        {
            ToTable("SubFingerprints", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.TrackId).HasColumnName(@"TrackId").HasColumnType("int").IsRequired();
            Property(x => x.SequenceNumber).HasColumnName(@"SequenceNumber").HasColumnType("int").IsRequired();
            Property(x => x.SequenceAt).HasColumnName(@"SequenceAt").HasColumnType("float").IsRequired();
            Property(x => x.HashTable0).HasColumnName(@"HashTable_0").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable1).HasColumnName(@"HashTable_1").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable2).HasColumnName(@"HashTable_2").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable3).HasColumnName(@"HashTable_3").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable4).HasColumnName(@"HashTable_4").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable5).HasColumnName(@"HashTable_5").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable6).HasColumnName(@"HashTable_6").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable7).HasColumnName(@"HashTable_7").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable8).HasColumnName(@"HashTable_8").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable9).HasColumnName(@"HashTable_9").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable10).HasColumnName(@"HashTable_10").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable11).HasColumnName(@"HashTable_11").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable12).HasColumnName(@"HashTable_12").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable13).HasColumnName(@"HashTable_13").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable14).HasColumnName(@"HashTable_14").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable15).HasColumnName(@"HashTable_15").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable16).HasColumnName(@"HashTable_16").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable17).HasColumnName(@"HashTable_17").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable18).HasColumnName(@"HashTable_18").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable19).HasColumnName(@"HashTable_19").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable20).HasColumnName(@"HashTable_20").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable21).HasColumnName(@"HashTable_21").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable22).HasColumnName(@"HashTable_22").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable23).HasColumnName(@"HashTable_23").HasColumnType("bigint").IsRequired();
            Property(x => x.HashTable24).HasColumnName(@"HashTable_24").HasColumnType("bigint").IsRequired();
            Property(x => x.Clusters).HasColumnName(@"Clusters").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);

            // Foreign keys
            HasRequired(a => a.Track).WithMany(b => b.SubFingerprints).HasForeignKey(c => c.TrackId).WillCascadeOnDelete(false); // FK_SubFingerprints_Tracks
        }
    }
}

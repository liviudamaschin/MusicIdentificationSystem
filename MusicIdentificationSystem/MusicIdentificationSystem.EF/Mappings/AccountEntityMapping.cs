using MusicIdentificationSystem.EF.Entities;

namespace MusicIdentificationSystem.EF.Mappings
{
    public class AccountEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AccountEntity>
    {
        public AccountEntityMapping()
            : this("dbo")
        {
        }

        public AccountEntityMapping(string schema)
        {
            ToTable("Accounts", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.AccountName).HasColumnName(@"AccountName").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);
            Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsOptional();
        }
    }
}

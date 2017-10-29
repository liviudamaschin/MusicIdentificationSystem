// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.6
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace MusicIdentificationSystem.EF.Mappings
{
    using MusicIdentificationSystem.EF.Context;
    using MusicIdentificationSystem.EF.Entities;

    // AccountStreamStations
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.32.0.0")]
    public partial class AccountStreamStationEntityMapping : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AccountStreamStationEntity>
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
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
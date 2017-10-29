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


namespace MusicIdentificationSystem.EF.Entities
{

    // Accounts
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.32.0.0")]
    public partial class AccountEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public string AccountName { get; set; } // AccountName (length: 250)
        public bool? IsActive { get; set; } // IsActive

        public AccountEntity()
        {
            IsActive = true;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
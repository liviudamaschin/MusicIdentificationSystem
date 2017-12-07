namespace MusicIdentificationSystem.EF.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public string AccountName { get; set; } // AccountName (length: 250)
        public bool? IsActive { get; set; } // IsActive
    }
}

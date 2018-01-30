namespace MusicIdentificationSystem.EF.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public string AccountName { get; set; } // AccountName (length: 50)
        public string FullName { get; set; } // Name (length: 250)
        public string Email { get; set; } // Email (length: 50)
        public string Address { get; set; } // Address (length: 250)
        public bool IsActive { get; set; } // IsActive
    }
}

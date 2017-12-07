namespace MusicIdentificationSystem.EF.Entities
{
    public class ClientEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public string Email { get; set; } // Email (length: 50)
        public string Address { get; set; } // Address (length: 250)
        public bool? IsActive { get; set; } // IsActive
    }
}

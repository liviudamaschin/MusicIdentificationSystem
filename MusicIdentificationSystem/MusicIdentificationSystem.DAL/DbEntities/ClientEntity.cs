using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.Client")]
    public class ClientEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("Name")]
        public string Name { get; set; } // Name (length: 50)
        [Column("Email")]
        public string Email { get; set; } // Email (length: 50)
        [Column("Address")]
        public string Address { get; set; } // Address (length: 250)
        [Column("IsActive")]
        public bool? IsActive { get; set; } // IsActive
    }
}

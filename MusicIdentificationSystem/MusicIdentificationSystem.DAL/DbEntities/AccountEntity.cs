using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.Account")]
    public class AccountEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("AccountName")]
        public string AccountName { get; set; } // AccountName (length: 250)
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        [Column("IsActive")]
        public bool? IsActive { get; set; } // IsActive
    }
}

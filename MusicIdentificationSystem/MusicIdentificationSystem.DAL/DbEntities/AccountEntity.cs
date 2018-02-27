using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.Accounts")]
    public class AccountEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("AccountName")]
        public string AccountName { get; set; } // AccountName (length: 250)
        [Column("FullName")]
        public string FullName { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        [Column("Phone")]
        public string Phone { get; set; } // Address (length: 50)
        [Column("IsActive")]
        public bool? IsActive { get; set; } // IsActive
    }
}

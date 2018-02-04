using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.AccountStreamStation")]
    public class AccountStreamStationEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("AccountId")]
        public int AccountId { get; set; } // AccountId
        [Column("StreamStationId")]
        public int StreamStationId { get; set; } // StreamStationId
        [Column("IsActive")]
        public bool IsActive { get; set; } // IsActive
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.AccountXTracks")]
    public class AccountXTrackEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("AccountId")]
        public int AccountId { get; set; } // AccountId
        [Column("TrackId")]
        public int TrackId { get; set; } // TrackId
        [Column("IsActive")]
        public bool IsActive { get; set; } // IsActive

        // Foreign keys

        public virtual AccountEntity Account { get; set; } // FK_AccountXTracks_Accounts

        public virtual TrackEntity Track { get; set; } // FK_AccountXTracks_Tracks
    }
}

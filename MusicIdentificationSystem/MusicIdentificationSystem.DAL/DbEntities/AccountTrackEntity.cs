using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.AccountTracks")]
    public class AccountTrackEntity
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

        public virtual AccountEntity Account { get; set; } // FK_AccountTracks_Accounts

        public virtual TrackEntity Track { get; set; } // FK_AccountTracks_Tracks
    }
}

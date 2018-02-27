
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.ApplicationSettings")]
    public class ApplicationSettingEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("KeyName")]
        public string KeyName { get; set; } // KeyName (length: 250)
        [Column("KeyValue")]
        public string KeyValue { get; set; } // KeyValue (length: 250)
        [Column("AccountId")]
        public int? AccountId { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }

        // Foreign keys

        /// <summary>
        /// Parent Account pointed by [Fingerprints].([AccountId]) (FK_Fingerprints_Accounts)
        /// </summary>
        public virtual AccountEntity Account { get; set; } // FK_Fingerprints_Accounts
    }
}

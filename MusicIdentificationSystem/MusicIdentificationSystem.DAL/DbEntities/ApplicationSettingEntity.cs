
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

        //public int? AccountId { get; set; }

        //public string Description { get; set; }

        //public bool IsActive { get; set; }
    }
}

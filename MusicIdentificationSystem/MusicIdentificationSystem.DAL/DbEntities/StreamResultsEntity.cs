
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.StreamResults")]
    public class StreamResultsEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public int StreamId { get; set; } // StreamId
        public int ResultId { get; set; } // ResultId
    }
}

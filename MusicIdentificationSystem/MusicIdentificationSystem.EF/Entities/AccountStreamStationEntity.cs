namespace MusicIdentificationSystem.EF.Entities
{
    public class AccountStreamStationEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public int AccountId { get; set; } // AccountId
        public int StreamStationId { get; set; } // StreamStationId
        public bool IsActive { get; set; } // IsActive
    }
}

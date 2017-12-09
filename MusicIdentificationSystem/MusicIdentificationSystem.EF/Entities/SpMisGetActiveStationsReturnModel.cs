
namespace MusicIdentificationSystem.EF.Entities
{

    public class SpMisGetActiveStationsReturnModel
    {
        public System.Int32 Id { get; set; }
        public System.String StationName { get; set; }
        public System.String URL { get; set; }
        public System.String LocalPath { get; set; }
        public System.Boolean? IsActive { get; set; }
        public System.String Description { get; set; }
        public System.Boolean? Running { get; set; }
        public System.String FileName { get; set; }
    }

}


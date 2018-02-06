using AutoMapper;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MusicIdentificationSystem.DAL.Repositories
{
    public class StreamStationRepository : GenericRepository2<StreamStationEntity>
    {
        private readonly IMapper mapper;

        //public StreamStationRepository(DatabaseContext context) : base(context)
        //{
        //}

        public List<ActiveStation> GetGetActiveStations()
        {
            List<ActiveStation> streamStationEntityList;
            
                streamStationEntityList = this.ExecuteProcedure<ActiveStation>("sp_mis_GetActiveStations").ToList();

            return streamStationEntityList;
        }

       
        //public StreamStationEntity GetById(int Id)
        //{
        //    using (var context = this.ResolveDbContext())
        //    {
        //        return context.StreamStations.FirstOrDefault(x => x.Id == Id);

        //    }
        //}
    }
}

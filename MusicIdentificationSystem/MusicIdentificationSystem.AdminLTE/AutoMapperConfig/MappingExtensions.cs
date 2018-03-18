using AutoMapper;
using MusicIdentificationSystem.AdminLTE.Models.Account;
using MusicIdentificationSystem.AdminLTE.Models.ApplicationSetting;
using MusicIdentificationSystem.AdminLTE.Models.StreamStation;
using MusicIdentificationSystem.AdminLTE.Models.Track;
using MusicIdentificationSystem.DAL.DbEntities;

namespace MusicIdentificationSystem.AdminLTE.AutoMapperConfig
{
    public static class MappingExtensions
    {
        public static ApplicationSettingModel ToModel(this ApplicationSettingEntity entity)
        {
            return Mapper.Map<ApplicationSettingEntity, ApplicationSettingModel>(entity);
        }

        public static ApplicationSettingEntity ToEntity(this ApplicationSettingModel model)
        {
            return Mapper.Map<ApplicationSettingModel, ApplicationSettingEntity>(model);
        }

        public static AccountModel ToModel(this AccountEntity entity)
        {
            return Mapper.Map<AccountEntity, AccountModel>(entity);
        }

        public static AccountEntity ToEntity(this AccountModel model)
        {
            return Mapper.Map<AccountModel, AccountEntity>(model);
        }

        public static StreamStationModel ToModel(this StreamStationEntity entity)
        {
            return Mapper.Map<StreamStationEntity, StreamStationModel>(entity);
        }

        public static StreamStationEntity ToEntity(this StreamStationModel model)
        {
            return Mapper.Map<StreamStationModel, StreamStationEntity>(model);
        }

        public static TrackModel ToModel(this TrackEntity entity)
        {
            return Mapper.Map<TrackEntity, TrackModel>(entity);
        }

        public static TrackEntity ToEntity(this TrackModel model)
        {
            return Mapper.Map<TrackModel, TrackEntity>(model);
        }
    }
}
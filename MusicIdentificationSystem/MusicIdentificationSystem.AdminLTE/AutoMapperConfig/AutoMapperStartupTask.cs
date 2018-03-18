using AutoMapper;
using MusicIdentificationSystem.AdminLTE.Models.Account;
using MusicIdentificationSystem.AdminLTE.Models.ApplicationSetting;
using MusicIdentificationSystem.AdminLTE.Models.StreamStation;
using MusicIdentificationSystem.AdminLTE.Models.Track;
using MusicIdentificationSystem.DAL.DbEntities;

namespace MusicIdentificationSystem.AdminLTE.AutoMapperConfig
{
    public static class AutoMapperStartupTask
    {
        public static void Execute()
        {
            Mapper.CreateMap<ApplicationSettingEntity, ApplicationSettingModel>()
                .ForMember(x => x.AccountName, action => action.Ignore());

            Mapper.CreateMap<ApplicationSettingModel, ApplicationSettingEntity>()
                .ForMember(x => x.Account, action => action.Ignore());

            Mapper.CreateMap<AccountEntity, AccountModel>();

            Mapper.CreateMap<AccountModel, AccountEntity>();

            Mapper.CreateMap<StreamStationEntity, StreamStationModel>();

            Mapper.CreateMap<StreamStationModel, StreamStationEntity>();

            Mapper.CreateMap<TrackEntity, TrackModel>()
                .ForMember(x => x.LengthText, action => action.Ignore());

            Mapper.CreateMap<TrackModel, TrackEntity>()
                .ForMember(x => x.Fingerprints, action => action.Ignore())
                .ForMember(x => x.Results, action => action.Ignore())
                .ForMember(x => x.SubFingerprints, action => action.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}
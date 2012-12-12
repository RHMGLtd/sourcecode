using System;
using AutoMapper;
using blackpoolgigs.common.Models;
using coolbunny.common.Interfaces;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;

namespace rhmg.AdministrateBlackpoolGigs.web.Services
{
    public class AutoMapperConfiguration : IConfigureAutomapper
    {
        public void Do()
        {
            Mapper.CreateMap<BandViewModel, BandMetadata>();
            Mapper.CreateMap<BandMetadata, BandViewModel>()
                .ForMember(vm => vm.Gigs, b => b.Ignore())
                .ForMember(vm => vm.Params, b => b.Ignore());
        }
    }
}
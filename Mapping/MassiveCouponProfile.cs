using AutoMapper;
using BaobabBackEndService.DTOs;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Mapping
{
    public class MassiveCouponProfile : Profile
    {
        public MassiveCouponProfile()
        {
            CreateMap<MassiveCoupon, RedeemDTO>();
            CreateMap<RedeemDTO, MassiveCoupon>()
            .ForMember(e => e.RedemptionDate, opt => opt.MapFrom(e => DateTime.Now))
            .ForMember(dest => dest.MassiveCouponCode, opt => opt.MapFrom(e => RandomCodeGenerator.CodeGenerator(10)));
        }
    }
}

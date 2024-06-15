using AutoMapper;
using BaobabBackEndService.DTOs;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Mapping
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<CouponDTO, Coupon>()
                .ForMember(e => e.StartDate, opt => opt.MapFrom(e => DateTime.Parse(e.StartDate)))
                .ForMember(e => e.ExpiryDate, opt => opt.MapFrom(e => DateTime.Parse(e.ExpiryDate)))
                .ForMember(e => e.CouponCode, opt => opt.MapFrom(e => string.IsNullOrWhiteSpace(e.CouponCode) ? RandomCodeGenerator.CodeGenerator(10) : e.CouponCode));
            ;

            CreateMap<Coupon, CouponDTO>()
                .ForMember(e => e.StartDate, opt => opt.MapFrom(e => e.StartDate.HasValue ? e.StartDate.Value.ToString("yyyy-MM-dd") : null))
                .ForMember(e => e.ExpiryDate, opt => opt.MapFrom(e => e.ExpiryDate.HasValue ? e.ExpiryDate.Value.ToString("yyyy-MM-dd") : null));
            // Mapper para editar cupón:
            CreateMap<CouponUpdateDTO, Coupon>();
        }
    }
}

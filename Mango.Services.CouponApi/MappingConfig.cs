using AutoMapper;
using Mango.Services.CouponApi.Models;
using Mango.Services.CouponApi.Models.Dto;

namespace Mango.Services.CouponApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapper = new MapperConfiguration(opt =>
            {
                opt.CreateMap<CouponDto, Coupon>().ReverseMap();

            });
            return mapper;
        }
    }

}

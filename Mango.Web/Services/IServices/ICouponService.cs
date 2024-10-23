using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto> GetAllCouponsAsync();
        Task<ResponseDto> CreateCouponAsync(CouponDto coupon);
        Task<ResponseDto> UpdateCouponAsync(CouponDto coupon);
        Task<ResponseDto> DeleteCouponAsync(int couponId);
        Task<ResponseDto> GetCouponByIdAsync(int couponId);
        Task<ResponseDto> GetCouponAsync(string couponCode);
    }
}

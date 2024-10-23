using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> list = new();

            ResponseDto? responseDto = await _couponService.GetAllCouponsAsync();

            if (responseDto != null && responseDto.IsSucces)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Result))!;

            }
            else
            {
                TempData["error"] = responseDto.ErrorMessage;

            }
            return View(list);
        }

        public IActionResult CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto dto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _couponService.CreateCouponAsync(dto);

                if (responseDto.IsSucces)
                {
                    TempData["success"] = "Coupon Created Successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = responseDto.ErrorMessage;

                }
            }
            return View(dto);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto? responseDto = await _couponService.GetCouponByIdAsync(couponId);

            if (responseDto != null && responseDto.IsSucces)
            {
                CouponDto coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result))!;

                return View(coupon);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto dto)
        {
            ResponseDto? responseDto = await _couponService.DeleteCouponAsync(dto.CouponId);

            if (responseDto != null && responseDto.IsSucces)
            {
                TempData["success"] = "Coupon Deleted Successfully";

                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
            }

            return NotFound();
        }
    }
}

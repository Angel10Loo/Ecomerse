using AutoMapper;
using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.Models;
using Mango.Services.CouponApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponApi.Controllers
{
	[Route("api/coupon")]
	[ApiController]
	public class CouponController : ControllerBase
	{
		private readonly AppDBContext _dbContext;
		private readonly IMapper _mapper;
		ResponseDto _responseDto;
		public CouponController(AppDBContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_responseDto = new();
		}

		[HttpGet]
		public ResponseDto Get()
		{
			try
			{
				IEnumerable<Coupon> coupons = _dbContext.Coupons.ToList();
				_responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
			}
			catch (Exception ex)
			{
				_responseDto.ErrorMessage = ex.Message;
				_responseDto.IsSucces = false;
			}
			return _responseDto;
		}

		[Route("{Id}")]
		[HttpGet]
		public ResponseDto Get(int Id)
		{
			try
			{
				Coupon coupon = _dbContext.Coupons.First(c => c.CouponId == Id);
				_responseDto.Result = _mapper.Map<CouponDto>(coupon);
			}
			catch (Exception ex)
			{
				_responseDto.ErrorMessage = ex.Message;
				_responseDto.IsSucces = false;
			}
			return _responseDto;
		}

		[Route("GetByCode/{code}")]
		[HttpGet]
		public ResponseDto Get(string code)
		{
			try
			{
				Coupon coupon = _dbContext.Coupons.First(c => c.CouponCode.ToLower() == code.ToLower());
				_responseDto.Result = _mapper.Map<CouponDto>(coupon);
			}
			catch (Exception ex)
			{
				_responseDto.ErrorMessage = ex.Message;
				_responseDto.IsSucces = false;
			}
			return _responseDto;
		}

		[HttpPost]
		public ResponseDto Post([FromBody] CouponDto dto)
		{
			try
			{
				Coupon coupon = _mapper.Map<Coupon>(dto);
				_dbContext.Coupons.Add(coupon);
				_dbContext.SaveChanges();
				_responseDto.Result = _mapper.Map<CouponDto>(coupon);
			}
			catch (Exception ex)
			{
				_responseDto.ErrorMessage = ex.Message;
				_responseDto.IsSucces = false;
			}
			return _responseDto;
		}

		[HttpPut]
		public ResponseDto Put([FromBody] CouponDto dto)
		{
			try
			{
				Coupon coupon = _mapper.Map<Coupon>(dto);
				_dbContext.Coupons.Update(coupon);
				_dbContext.SaveChanges();
				_responseDto.Result = _mapper.Map<CouponDto>(coupon);
			}
			catch (Exception ex)
			{
				_responseDto.ErrorMessage = ex.Message;
				_responseDto.IsSucces = false;
			}
			return _responseDto;
		}
		[Route("{id}")]
		[HttpDelete]
		public ResponseDto Delete([FromRoute] int Id)
		{
			try
			{
				Coupon coupon = _dbContext.Coupons.First(c => c.CouponId == Id);
				_dbContext.Coupons.Remove(coupon);
				_dbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				_responseDto.ErrorMessage = ex.Message;
				_responseDto.IsSucces = false;
			}
			return _responseDto;
		}
	}
}

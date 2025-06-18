using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers
{
    public class DiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountQueryHandler> _logger;
        public DiscountQueryHandler(IDiscountRepository discountRepository,ILogger<DiscountQueryHandler> logger)
        {
            _discountRepository = discountRepository;
            _logger = logger;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount not found for product: {request.ProductName}"));

            }
            _logger.LogInformation("Discount retrieved successfully for product: {ProductName}", request.ProductName);
            return new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            }; 
        }
    }
    
}

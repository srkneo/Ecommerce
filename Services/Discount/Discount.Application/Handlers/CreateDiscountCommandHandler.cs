using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        public IMapper _mapper { get; }
        private readonly ILogger<CreateDiscountCommandHandler> _logger;
        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper,ILogger<CreateDiscountCommandHandler> logger)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _discountRepository.CreateDiscount(coupon);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            _logger.LogInformation("Discount created successfully for product: {ProductName}", coupon.ProductName);

            return couponModel;
        }
    }
}

using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Queries
{
    public class CreateDiscountCommand : IRequest<CouponModel>
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}

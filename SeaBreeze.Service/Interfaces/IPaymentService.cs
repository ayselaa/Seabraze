using SeaBreeze.Domain.Entity.Payment;
using static SeaBreeze.Service.Services.PaymentService;

namespace SeaBreeze.Service.Interfaces
{
    public interface IPaymentService
    {
        Task<PayriffOrder> CreateOrder(decimal amount);
        Task<string> CheckOrderStatus(CheckOrderDto checkOrderDto);
    }
}

using WebBanHang0.Models;

namespace WebBanHang0.Areas.Admin.Models.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
        public IEnumerable<OrderModel> OrderModel { get; set; }
    }
}

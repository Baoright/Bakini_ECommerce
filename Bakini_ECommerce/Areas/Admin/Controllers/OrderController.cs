using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang0.Repository;

namespace WebBanHang0.Areas.Admin.Controllers
{
	[Area("Admin")]
<<<<<<< Updated upstream
	[Authorize]
=======
    [Authorize(Roles = "Admin, Seller")]
>>>>>>> Stashed changes
	public class OrderController : Controller
	{
		private readonly DatabaseContext _databaseContext;

		public OrderController(DatabaseContext context)
		{
			_databaseContext = context;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _databaseContext.Orders.OrderByDescending(p => p.Id).ToListAsync());
		}
		public async Task<IActionResult> ViewOrder(string ordercode)
		{
			var DetailsOrder = await _databaseContext.OrdersDetails.Include(o => o.Product).Where(o => o.OrderCode == ordercode).ToListAsync();
			return View(DetailsOrder);
		}
		[HttpPost]
		[Route("UpdateOrder")]
		public async Task<IActionResult> UpdateOrder(string ordercode, int status)
		{
			var order = await _databaseContext.Orders.FirstOrDefaultAsync(o => o.OrderCode == ordercode);
			if (order == null)
			{
				return NotFound();
			}
			order.Status = status;
			try
			{
				await _databaseContext.SaveChangesAsync();
				return Ok(new { success = true, message = "Done" });

			}
			catch (Exception ex)
			{
				return StatusCode(500, "Error");
			}
		}

		[HttpPost]
		[Route("DeleteOrder")]
		public async Task<IActionResult> DeleteOrder(string ordercode)
		{
			// Tìm đơn hàng theo mã đơn hàng
			var order = await _databaseContext.Orders.FirstOrDefaultAsync(o => o.OrderCode == ordercode);
			if (order == null)
			{
				return NotFound(new { success = false, message = "Order not found" });
			}

			// Tìm các chi tiết của đơn hàng
			var orderDetails = await _databaseContext.OrdersDetails.Where(od => od.OrderCode == ordercode).ToListAsync();

			// Xóa chi tiết đơn hàng
			_databaseContext.OrdersDetails.RemoveRange(orderDetails);

			// Xóa đơn hàng
			_databaseContext.Orders.Remove(order);

			try
			{
				// Lưu thay đổi vào cơ sở dữ liệu
				await _databaseContext.SaveChangesAsync();
				return Ok(new { success = true, message = "Order deleted successfully" });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = "Error deleting order" });
			}
		}

	}
}

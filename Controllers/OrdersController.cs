using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarMaintenance.Data;
using CarMaintenance.Models;
using CarMaintenance.Models.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace CarMaintenance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<Order>>), 200)]
        [SwaggerOperation(
            Summary = "Get All Orders",
            Description = "Returns all orders with full details including user, vehicle, service, status, address, and phone number."
        )]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return Ok(new ApiResponse<List<Order>>(
                true,
                "Orders fetched successfully",
                orders
            ));
        }

        /// <summary>
        /// Get a specific order by its ID.
        /// </summary>
        /// <param name="id">The order ID.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Order>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [SwaggerOperation(
            Summary = "Get Order By ID",
            Description = "Returns a single order by its ID with all details."
        )]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound(new ApiResponse<object>(
                    false,
                    "Order not found",
                    null!
                ));
            }

            return Ok(new ApiResponse<Order>(
                true,
                "Order fetched successfully",
                order
            ));
        }

        /// <summary>
        /// Create a new order.
        /// </summary>
        /// <param name="dto">The order creation data.</param>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Order>), 201)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [SwaggerOperation(
            Summary = "Create Order",
            Description = "Creates a new order with userId, vehicleId, serviceId, address, and phoneNumber. Status defaults to 'New'."
        )]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Invalid data",
                    ModelState
                ));
            }

            var order = new Order
            {
                UserId = dto.UserId,
                VehicleId = dto.VehicleId,
                ServiceId = dto.ServiceId,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                OrderStatus = OrderStatus.New,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = order.Id },
                new ApiResponse<Order>(
                    true,
                    "Order created successfully",
                    order
                ));
        }

        /// <summary>
        /// Update the status of an existing order.
        /// </summary>
        /// <param name="id">The order ID.</param>
        /// <param name="dto">The new order status.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Order>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [SwaggerOperation(
            Summary = "Update Order Status",
            Description = "Updates only the status of an existing order. Valid statuses: New, OnTheWay, UnderProcess, Completed, Canceled."
        )]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Invalid data",
                    ModelState
                ));
            }

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound(new ApiResponse<object>(
                    false,
                    "Order not found",
                    null!
                ));
            }

            order.OrderStatus = dto.OrderStatus;
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<Order>(
                true,
                "Order status updated successfully",
                order
            ));
        }

        /// <summary>
        /// Cancel (delete) an order.
        /// </summary>
        /// <param name="id">The order ID.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [SwaggerOperation(
            Summary = "Cancel Order",
            Description = "Cancels an order by setting its status to 'Canceled'. The order is not permanently deleted from the database."
        )]
        public async Task<IActionResult> Cancel(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound(new ApiResponse<object>(
                    false,
                    "Order not found",
                    null!
                ));
            }

            order.OrderStatus = OrderStatus.Canceled;
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<object>(
                true,
                "Order canceled successfully",
                order
            ));
        }
    }
}

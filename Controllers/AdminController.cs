using Microsoft.AspNetCore.Mvc;
using CarMaintenance.Models;
using Swashbuckle.AspNetCore.Annotations;
    


[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "Admin")]
public class AdminController : ControllerBase
{
    /// <summary>
    /// Get the admin dashboard data including stats, latest requests, technicians, and notifications.
    /// </summary>
    [HttpGet("/admin")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    [SwaggerOperation(
        Summary = "Get Admin Dashboard",
        Description = "Returns the full admin dashboard data including revenue stats, latest service requests, technician info, and notifications."
    )]
    public IActionResult GetDashboard()
    {
        var data = new
        {
            stats = new
            {
                totalRevenue = 45320,
                totalOrders = 34,
                todayOrders = 89,
                activeOrders = 47,
                pendingOrders = 23,
                totalRequests = 1247
            },

            latestRequests = new List<object>
            {
                new {
                    id = 1,
                    customerName = "أحمد محمد",
                    service = "تغيير زيت المحرك",
                    price = 350,
                    status = "pending",
                    date = "2026-04-19"
                },
                new {
                    id = 2,
                    customerName = "حسن علي",
                    service = "تغيير البطارية",
                    price = 500,
                    status = "completed",
                    date = "2026-04-18"
                },
                new {
                    id = 3,
                    customerName = "محمد سامي",
                    service = "صيانة فرامل",
                    price = 250,
                    status = "inProgress",
                    date = "2026-04-17"
                },
                new {
                    id = 4,
                    customerName = "يوسف أحمد",
                    service = "خدمة طوارئ",
                    price = 300,
                    status = "pending",
                    date = "2026-04-16"
                }
            },

            technicians = new List<object>
            {
                new {
                    name = "محمد أحمد",
                    rating = 4.8,
                    completedJobs = 145,
                    status = "available"
                },
                new {
                    name = "حسن محمود",
                    rating = 4.6,
                    completedJobs = 120,
                    status = "busy"
                },
                new {
                    name = "علي يوسف",
                    rating = 4.5,
                    completedJobs = 98,
                    status = "available"
                }
            },

         notifications = new List<object>
    {
        new {
            id = 1,
            title = "طلب طوارئ جديد",
            description = "طلب طوارئ في الجيزة - الهرم، يحتاج موافقة فورية",
            time = "منذ دقيقتين",
            type = "urgent",
            color = "red",
            icon = "alert"
        },
        new {
            id = 2,
            title = "فني غير متاح",
            description = "الفني عمر سعيد أبلغ عن عطل في السيارة",
            time = "منذ 15 دقيقة",
            type = "warning",
            color = "yellow",
            icon = "user"
        },
        new {
            id = 3,
            title = "دفعة جديدة",
            description = "تم استلام دفعة بقيمة 500 جنيه من العميل محمد أحمد",
            time = "منذ 30 دقيقة",
            type = "success",
            color = "green",
            icon = "money"
        }
    }
        };

        return Ok(new ApiResponse<object>(
            true,
            "Dashboard loaded successfully",
            data
        ));
    }

    /// <summary>
    /// Get all admin notifications.
    /// </summary>
    [HttpGet("notifications")]
    [ProducesResponseType(typeof(ApiResponse<List<string>>), 200)]
    [SwaggerOperation(
        Summary = "Get Notifications",
        Description = "Returns a list of admin notifications such as new orders, technician assignments, and completed orders."
    )]
    public IActionResult GetNotifications()
    {
        var notifications = new List<string>
        {
            "New order received",
            "Technician assigned",
            "Order completed"
        };

        return Ok(new ApiResponse<List<string>>(
            true,
            "Notifications fetched",
            notifications
        ));
    }

    /// <summary>
    /// Search orders by customer name or service name.
    /// </summary>
    /// <param name="query">Optional search query to filter orders by customer name or service.</param>
    [HttpGet("/searchOrders")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    [SwaggerOperation(
        Summary = "Search Orders",
        Description = "Searches orders by customer name or service name. Returns all orders if no query is provided."
    )]
    public IActionResult Search([FromQuery] string? query)
    {
        var orders = new List<Order>
    {
        new Order { Id = 1, CustomerName = "Ahmed", Service = "Oil Change", Price = 350, Status = "pending" },
        new Order { Id = 2, CustomerName = "Ali", Service = "Battery Change", Price = 500, Status = "completed" }
    };
    
         if (string.IsNullOrWhiteSpace(query))
            return Ok(orders); 

       var result = orders
        .Where(o => o.CustomerName.ToLower().Contains(query.ToLower()) 
                 || o.Service.ToLower().Contains(query.ToLower()))
        .ToList();
        return Ok(new ApiResponse<object>(
            true,
            "Search completed",
            result
        ));
    }

}

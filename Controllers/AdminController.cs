using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
 [HttpGet("/admin")]
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
                title = "طلب جديد",
                message = "تم إضافة طلب جديد",
                type = "info"
            },
            new {
                title = "تم قبول الطلب",
                message = "تم قبول أحد الطلبات",
                type = "success"
            },
            new {
                title = "طلب مرفوض",
                message = "تم رفض طلب",
                type = "warning"
            }
        }
    };

    return Ok(new ApiResponse<object>(
        true,
        "Dashboard loaded successfully",
        data
    ));
}
[HttpGet("notifications")]
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
[HttpGet("search")]
public IActionResult Search(string query)
{
    var result = new List<string>
    {
        $"Result for {query} 1",
        $"Result for {query} 2"
    };

    return Ok(new ApiResponse<List<string>>(
        true,
        "Search completed",
        result
    ));
}

}

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
 [HttpGet]
public IActionResult GetDashboard()
{
    var data = new
    {
        totalOrders = 120,
        totalUsers = 45,
        pendingOrders = 10
    };

    return Ok(new ApiResponse<object>(
        true,
        "Dashboard data fetched",
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

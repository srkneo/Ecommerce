using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {

    }
}

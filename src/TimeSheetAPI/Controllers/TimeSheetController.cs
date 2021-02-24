using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TimeSheetAPI.Controllers
{
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        public TimeSheetController(ILogger<TimeSheetController> logger)
        {
            
        }
    }
}

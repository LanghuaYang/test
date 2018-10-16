using System.Web.Http;
using solution.ControllerLogic;
using solution.Models;

namespace solution.Controllers
{
    public class TestController : ApiController
    {
        public IDataExtractor DataExtractor { get; set; }

        [AllowAnonymous]
        [HttpGet]
        [HttpPost]
        public ApiResult<ViewModel> GetDataFromText(string data)
        {
            return DataExtractor.GetDataFromText(data);
        }
    }
}

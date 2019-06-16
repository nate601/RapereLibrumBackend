using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookInfoFetchController : ControllerBase
    {
        [HttpGet("{Isbn}")]
        public ActionResult<backend.Apis.GoogleApi.GoogleBookInfo> GetBookInfo(string Isbn)
        {
            Apis.GoogleApi.GoogleBookInfo value = backend.Apis.GoogleApi.GetBookInfo(Isbn);
            if (value == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(value);
        }
    }

}
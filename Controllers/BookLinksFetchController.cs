using backend.Apis;
using Microsoft.AspNetCore.Mvc;
[Route("api/[controller]")]
[ApiController]
public class BookLinksFetchController : ControllerBase
{
    [HttpGet]
    public ActionResult<LibgenApi.RowInfo> Get(string bookTitle)
    {
        var bookInfos = LibgenApi.GetLibgen(bookTitle);
        if (bookInfos != null)
            return new OkObjectResult(bookInfos);
        return new NotFoundResult();
    }


}
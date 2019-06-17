using Microsoft.AspNetCore.Mvc;
[Route("api/[controller]")]
[ApiController]
public class BookLinksFetchController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get(string bookTitle)
    {





        return new NotFoundResult();
    }


}
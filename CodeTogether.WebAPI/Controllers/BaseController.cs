using CodeTogether.DB;
using Microsoft.AspNetCore.Mvc;


namespace CodeTogether.WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public abstract class BaseController: ControllerBase
	{
		protected CodeTogetherDbContext _dbContext { get; set; }

		public BaseController(CodeTogetherDbContext dbContext)
		{
			_dbContext = dbContext;
		}
	}
}

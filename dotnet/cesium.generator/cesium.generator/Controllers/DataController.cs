using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cesium.generator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        [HttpGet]
        [Route("SplitLine")]
        public JsonResult SplitLine(double x1, double y1, double x2, double y2)
        {
            double[] linePoints = DataGenerator.SplitLine(x1, y1, x2, y2);
            return Json(linePoints);
        }

        [HttpGet]
        [Route("GenerateEllipse")]
        public JsonResult GenerateEllipse(double x1, double y1, double x2, double y2, int amountOfPoints)
        {
            double[] ellipsePoints = DataGenerator.GenerateEllipse(x1, y1, x2, y2, amountOfPoints);
            return Json(ellipsePoints);
        }
    }
}

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
        public IActionResult SplitLine(double x1, double y1, double x2, double y2)
        {
            if (!IsValidCoordinate(x1) || !IsValidCoordinate(y1) || !IsValidCoordinate(x2) || !IsValidCoordinate(y2))
            {
                return BadRequest("Invalid coordinates provided.");
            }

            double[] linePoints = DataGenerator.SplitLine(x1, y1, x2, y2);
            return Json(linePoints);
        }

        [HttpGet]
        [Route("GenerateEllipse")]
        public IActionResult GenerateEllipse(double x1, double y1, double x2, double y2, int amountOfPoints)
        {
            if (!IsValidCoordinate(x1) || !IsValidCoordinate(y1) || !IsValidCoordinate(x2) || !IsValidCoordinate(y2))
            {
                return BadRequest("Invalid coordinates provided.");
            }

            if (amountOfPoints <= 0)
            {
                return BadRequest("Amount of points must be a positive integer.");
            }

            double[] ellipsePoints = DataGenerator.GenerateEllipse(x1, y1, x2, y2, amountOfPoints);
            return Json(ellipsePoints);
        }

        private bool IsValidCoordinate(double coordinate)
        {
            return coordinate<180.0f && coordinate>-180.0f; 
        }
    }
}

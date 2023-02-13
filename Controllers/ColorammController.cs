using API_Coloramm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using API_Coloramm.Models.CustomCode;
using API_Coloramm.Controllers.Services;

namespace API_Coloramm.Controllers
{
    [Route("api/Coloramm")]
    [ApiController]
    public class ColorammController
    {
        private readonly IColorammService _colorammService;

        public ColorammController(IColorammService colorammService)
        {
            this._colorammService = colorammService;
        }

        [HttpPost]
        [Route("GetColor")]
        public async Task<IActionResult> GetColor([FromBody] ModelRequest request, bool app = false)
        {
            CustomHttpResponse result = null;
            ModelResponse response = null;
            try
            {
                // logica...


                result = await _colorammService.GetColor(request, app);


                // ...logica
            }
            catch (Exception ex)
            {
                // es:
                // Log.Error($"{ex.Message} -- {ex.StackTrace} -- DateTime.Now.ToShortDateString()");
            }
            finally 
            {
            }

            return result;
        }
    }
}

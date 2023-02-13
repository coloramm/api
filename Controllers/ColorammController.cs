using API_Coloramm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using API_Coloramm.Models.CustomCode;
using API_Coloramm.Controllers.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace API_Coloramm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorammController
    {
        private readonly IColorammService _colorammService;

        public ColorammController(IColorammService colorammService)
        {
            this._colorammService = colorammService;
        }

        /// <summary>
        /// GetColor
        /// </summary>
        /// <param name="request"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Get_Color")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

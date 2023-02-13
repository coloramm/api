using API_Coloramm.Models;
using API_Coloramm.Models.CustomCode;

namespace API_Coloramm.Controllers.Services
{
    public class ColorammService : IColorammService
    {
        public async Task<CustomHttpResponse> GetColor(ModelRequest request, bool app)
        {
            ModelResponse response = new ModelResponse();

            try
            {
                // logica...

                // es:
                // se la richiesta proviene da un app o da un PC
                if (!app)
                    response.middleColor = request.startColor + "  +  " + request.endColor;
                else
                    response.middleColor = request.endColor + "  +  " + request.startColor;


                // ...logica


                if (response == null)
                    return CustomHttpResponse.BuildError(404, "Nessun Colore");
                else
                    return CustomHttpResponse.BuildOk(200, response.middleColor, "OK");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}

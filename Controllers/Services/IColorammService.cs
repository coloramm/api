using API_Coloramm.Models;
using API_Coloramm.Models.CustomCode;

namespace API_Coloramm.Controllers.Services
{
    public interface IColorammService
    {
        public Task<CustomHttpResponse> GetColor(ModelRequest request, bool app);
    }
}

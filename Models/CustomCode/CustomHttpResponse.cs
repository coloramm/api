using Microsoft.AspNetCore.Mvc;

namespace API_Coloramm.Models.CustomCode
{
    public class CustomHttpResponse : ObjectResult
    {
        private readonly object? value;

        public CustomHttpResponse(object? value) : base(value)
        {
            this.value = value;
        }

        public static CustomHttpResponse BuildOk(int statusCode, object data, string message)
        {

            CustomHttpResponse httpresponse = new CustomHttpResponse(new { Message = message, Data = data, StatusCode = statusCode, Success = true });   //BODY

            httpresponse.StatusCode = statusCode;   // HEADER

            return httpresponse;
        }


        public static CustomHttpResponse BuildError(int statusCode, string message)
        {
            CustomHttpResponse httpresponse = new CustomHttpResponse(new { Message = message, StatusCode = statusCode, Success = false });   //BODY

            httpresponse.StatusCode = statusCode;    // HEADER

            return httpresponse;
        }



        public static CustomHttpResponse BuildDataAnnotationsError(List<string> listDataAnnotationErrorMessage)
        {
            CustomHttpResponse httpresponse = new CustomHttpResponse(new { Message = listDataAnnotationErrorMessage, StatusCode = 422, Success = false });   //BODY

            httpresponse.StatusCode = 422;    // HEADER

            return httpresponse;
        }
    }
}

using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;


namespace e_learning.Services
{
    public class LessonService(IHttpContextAccessor httpContextAccessor)
        : BaseService(httpContextAccessor), ILessonService
    {
        private readonly HttpRequest _request = httpContextAccessor.HttpContext.Request;
        private readonly HttpResponse _response = httpContextAccessor.HttpContext.Response;

        public async Task<LessonModel> GetAllLessonAsync()
        {
            throw new NotImplementedException();
        }

        // public async Task<IActionResult> PlayVideo(string filePath)
        // {
        //     
        //     
        //     var fileLength = new FileInfo(filePath).Length;
        //     using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        //     {
        //         if (_request.Headers.ContainsKey("Range"))
        //         {
        //             var rangeHeader = _request.Headers["Range"].ToString();
        //             var range = rangeHeader.Replace("bytes=", "").Split('-');
        //             var start = long.Parse(range[0]);
        //             var end = range.Length > 1 && !string.IsNullOrEmpty(range[1])
        //                 ? long.Parse(range[1])
        //                 : fileLength - 1;
        //
        //             fileStream.Seek(start, SeekOrigin.Begin);
        //
        //             var contentLength = end - start + 1;
        //
        //             _response.StatusCode = 206;
        //             _response.Headers[HeaderNames.AcceptRanges] = "bytes";
        //             _response.Headers[HeaderNames.ContentLength] = contentLength.ToString();
        //             _response.Headers[HeaderNames.ContentRange] = $"bytes {start}-{end}/{fileLength}";
        //             _response.Headers[HeaderNames.ContentType] = "video/mp4";
        //         }
        //
        //         return new FileStreamResult(fileStream, "video/mp4");
        //     }
        // }

        public async Task<IActionResult> PlayVideo(string filePath)
        {
            var fileLength = new FileInfo(filePath).Length;
            FileStream fileStream = null;


            fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            if (_request.Headers.ContainsKey("Range"))
            {
                var rangeHeader = _request.Headers["Range"].ToString();
                var range = rangeHeader.Replace("bytes=", "").Split('-');
                var start = long.Parse(range[0]);
                var end = range.Length > 1 && !string.IsNullOrEmpty(range[1])
                    ? long.Parse(range[1])
                    : fileLength - 1;

                fileStream.Seek(start, SeekOrigin.Begin);

                var contentLength = end - start + 1;


                _response.StatusCode = 206;
                _response.Headers[HeaderNames.AcceptRanges] = "bytes";
                _response.Headers[HeaderNames.ContentLength] = contentLength.ToString();
                _response.Headers[HeaderNames.ContentRange] = $"bytes {start}-{end}/{fileLength}";
                _response.Headers[HeaderNames.ContentType] = "video/mp4";
            }

            return new FileStreamResult(fileStream, "video/mp4");
        }
    }
}
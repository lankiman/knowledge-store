using e_learning.Data;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.IO;


namespace e_learning.Services
{
    public class LessonService(IHttpContextAccessor httpContextAccessor, ELearningDbContext eLearningContext)
        : BaseService(httpContextAccessor, eLearningContext), ILessonService
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

        // public async Task<IActionResult> PlayVideo(string filePath)
        // {
        //     var fileLength = new FileInfo(filePath).Length;
        //     FileStream fileStream = null;
        //
        //
        //     fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        //
        //     if (_request.Headers.ContainsKey("Range"))
        //     {
        //         var rangeHeader = _request.Headers["Range"].ToString();
        //         var range = rangeHeader.Replace("bytes=", "").Split('-');
        //         var start = long.Parse(range[0]);
        //         var end = range.Length > 1 && !string.IsNullOrEmpty(range[1])
        //             ? long.Parse(range[1])
        //             : fileLength - 1;
        //
        //         fileStream.Seek(start, SeekOrigin.Begin);
        //
        //         var contentLength = end - start + 1;
        //
        //
        //         _response.StatusCode = 206;
        //         _response.Headers[HeaderNames.AcceptRanges] = "bytes";
        //         _response.Headers[HeaderNames.ContentLength] = contentLength.ToString();
        //         _response.Headers[HeaderNames.ContentRange] = $"bytes {start}-{end}/{fileLength}";
        //         _response.Headers[HeaderNames.ContentType] = "video/mp4";
        //     }
        //
        //     return new FileStreamResult(fileStream, "video/mp4");
        // }

        public async Task<IActionResult> PlayVideo(string videoId)
        {
            var filePath = string.Empty;

            try
            {
                var path = eLearningContext.Lessons.Where(l => l.LessonId == videoId).Select(c => c.LessonVideoUrl)
                    .SingleOrDefaultAsync().Result;

                if (path == null)
                {
                    return new BadRequestResult();
                }

                filePath = path;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = "Server Error Occured" })
                    { StatusCode = 500 };
            }

            if (!File.Exists(filePath))
            {
                return new ObjectResult(new { Message = "Server Error Occured" })
                    { StatusCode = 500 };
            }


            var fileLength = new FileInfo(filePath).Length;

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
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

                    var buffer = new byte[64 * 1024];
                    var bytesRemaining = contentLength;

                    while (bytesRemaining > 0)
                    {
                        var bytesRead =
                            await fileStream.ReadAsync(buffer, 0, (int)Math.Min(buffer.Length, bytesRemaining));
                        if (bytesRead == 0)
                        {
                            break;
                        }

                        await _response.Body.WriteAsync(buffer, 0, bytesRead);
                        bytesRemaining -= bytesRead;
                    }
                }
                else
                {
                    _response.Headers[HeaderNames.ContentLength] = fileLength.ToString();
                    _response.Headers[HeaderNames.ContentType] = "video/mp4";

                    await fileStream.CopyToAsync(_response.Body);
                }

                return new EmptyResult();
            }
        }
    }
}
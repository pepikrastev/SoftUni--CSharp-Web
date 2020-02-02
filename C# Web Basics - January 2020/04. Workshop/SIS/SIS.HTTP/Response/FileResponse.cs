using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Response
{
    public class FileResponse : HttpResponse
    {
        public FileResponse(byte[] fileContent, string contentType)
        {
            this.StatusCode = HttpResponseCode.Ok;
            byte[] byteData = fileContent;
            this.Body = byteData;
            this.Headers.Add(new Header("Content-Type", contentType));
            this.Headers.Add(new Header("Content-Length", this.Body.Length.ToString()));
        }
    }
}

using kinoba.business;
using kinoba.business.Domain;
using kinoba.business.Managers;
using kinoba.web.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace kinoba.web.Controllers
{
    public class PhotoImagesController : ApiController
    {
        // GET: api/PhotoImages
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PhotoImages/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PhotoImages
        //public Task<HttpResponseMessage> Post()
        //{
        //    HttpRequestMessage request = this.Request;
        //    if (!request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads");
        //    var provider = new MultipartFormDataStreamProvider(root);

        //    var task = request.Content.ReadAsMultipartAsync(provider).
        //        ContinueWith<HttpResponseMessage>(o =>
        //        {

        //            string file1 = provider.BodyPartFileNames.First().Value;
        //            // this is the file name on the server where the file was saved 

        //            return new HttpResponseMessage()
        //            {
        //                Content = new StringContent("File uploaded.")
        //            };
        //        }
        //    );

        //    return task; 
        //}

        public HttpResponseMessage Post(int id)
        {
            var specialistMediaManager = new SpecialistMediaManager();

            HttpResponseMessage result = null;
            var response = new List<SpecialistPhoto>();

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                for (var i = 0 ; i < httpRequest.Files.Count; i++)
                {
                    var postedFile = httpRequest.Files[i];
                    var imagesCount = specialistMediaManager.GetImagesCount(id);
                    if (imagesCount > 9)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { result = false, msg = "Количество фото превысило лимит в 9 штук" });
                    }

                    var filePath = Kimages.GetUploadPathFile(id, postedFile.FileName); // HttpContext.Current.Server.MapPath("~/images" + postedFile.FileName);

                    var di = new DirectoryInfo(Path.GetDirectoryName(filePath));
                    if (!di.Exists)
                        di.Create();

                    postedFile.SaveAs(filePath);

                    var fileName = Path.GetFileName(filePath);
                    var url = Kimages.GetPhotoUrl(id, fileName);
                    var addedPhoto = specialistMediaManager.AddPhoto(id, fileName, url);
                    //addedPhoto.Url = Kimages.GetPhotoUrl(id, fileName);

                    response.Add(addedPhoto);
                    //response.Add(new SpecialistMedia() { I = addedMedia.Id, Url = Kimages.GetPhotoUrl(id, fileName) });
                }

                //response.Result = true;
                result = Request.CreateResponse(HttpStatusCode.Created, response);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }

        // PUT: api/PhotoImages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PhotoImages/5
        public void Delete(int id)
        {
        }
    }

    public class UploadResponse
    {
        //public bool Result { get; set; }
        public string Url { get; set; }
        public int PhotoId { get; set; }
        //public string  Msg { get; set; }
    }
}

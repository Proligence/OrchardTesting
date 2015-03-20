namespace Proligence.Orchard.Testing
{
    using System.IO;
    using System.Web.Mvc;
    
    public static class AssertActionResult
    {
        public static void IsHttpNotFound(ActionResult actionResult)
        {
            GenericAssert.InstanceOf<HttpNotFoundResult>(actionResult);
        }

        public static void IsHttpUnauthorized(ActionResult actionResult)
        {
            GenericAssert.InstanceOf<HttpUnauthorizedResult>(actionResult);
        }

        public static void IsRedirect(ActionResult actionResult, string targetUrl)
        {
            GenericAssert.InstanceOf<RedirectResult>(actionResult);

            var redirectResult = (RedirectResult)actionResult;
            GenericAssert.AreEqual(targetUrl, redirectResult.Url);
        }

        public static void IsRedirect(ActionResult actionResult, string targetUrl, bool permanent)
        {
            GenericAssert.InstanceOf<RedirectResult>(actionResult);

            var redirectResult = (RedirectResult)actionResult;
            GenericAssert.AreEqual(targetUrl, redirectResult.Url);
            GenericAssert.AreEqual(permanent, redirectResult.Permanent);
        }

        public static void IsFile(ActionResult actionResult, string fileName)
        {
            GenericAssert.InstanceOf<FileStreamResult>(actionResult);

            var fileStreamResult = (FileStreamResult)actionResult;
            GenericAssert.AreEqual(fileName, fileStreamResult.FileDownloadName);
        }
        
        public static void IsFile(ActionResult actionResult, string fileName, string contentType)
        {
            GenericAssert.InstanceOf<FileStreamResult>(actionResult);

            var fileStreamResult = (FileStreamResult)actionResult;
            GenericAssert.AreEqual(fileName, fileStreamResult.FileDownloadName);
            GenericAssert.AreEqual(contentType, fileStreamResult.ContentType);
        }

        public static void IsFile(ActionResult actionResult, string fileName, string contentType, Stream fileStream)
        {
            GenericAssert.InstanceOf<FileStreamResult>(actionResult);
            
            var fileStreamResult = (FileStreamResult)actionResult;
            GenericAssert.AreEqual(fileName, fileStreamResult.FileDownloadName);
            GenericAssert.AreEqual(contentType, fileStreamResult.ContentType);
            GenericAssert.AreSame(fileStream, fileStreamResult.FileStream);
        }
    }
}
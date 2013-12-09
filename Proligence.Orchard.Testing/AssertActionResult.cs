namespace Proligence.Orchard.Testing
{
    using System.IO;
    using System.Web.Mvc;
    using NUnit.Framework;

    public static class AssertActionResult
    {
        public static void IsHttpNotFound(ActionResult actionResult)
        {
            Assert.That(actionResult, Is.InstanceOf<HttpNotFoundResult>());
        }

        public static void IsRedirect(ActionResult actionResult, string targetUrl)
        {
            Assert.That(actionResult, Is.InstanceOf<RedirectResult>());

            var redirectResult = (RedirectResult)actionResult;
            Assert.That(redirectResult.Url, Is.EqualTo("http://target"));
        }

        public static void IsRedirect(ActionResult actionResult, string targetUrl, bool permanent)
        {
            Assert.That(actionResult, Is.InstanceOf<RedirectResult>());

            var redirectResult = (RedirectResult)actionResult;
            Assert.That(redirectResult.Url, Is.EqualTo("http://target"));
            Assert.That(redirectResult.Permanent, Is.EqualTo(permanent));
        }

        public static void IsFile(ActionResult actionResult, string fileName)
        {
            Assert.That(actionResult, Is.InstanceOf<FileStreamResult>());

            var fileStreamResult = (FileStreamResult)actionResult;
            Assert.That(fileStreamResult.FileDownloadName, Is.EqualTo(fileName));
        }
        
        public static void IsFile(ActionResult actionResult, string fileName, string contentType)
        {
            Assert.That(actionResult, Is.InstanceOf<FileStreamResult>());

            var fileStreamResult = (FileStreamResult)actionResult;
            Assert.That(fileStreamResult.FileDownloadName, Is.EqualTo(fileName));
            Assert.That(fileStreamResult.ContentType, Is.EqualTo(contentType));
        }

        public static void IsFile(ActionResult actionResult, string fileName, string contentType, Stream fileStream)
        {
            Assert.That(actionResult, Is.InstanceOf<FileStreamResult>());
            
            var fileStreamResult = (FileStreamResult)actionResult;
            Assert.That(fileStreamResult.FileDownloadName, Is.EqualTo(fileName));
            Assert.That(fileStreamResult.ContentType, Is.EqualTo(contentType));
            Assert.That(fileStreamResult.FileStream, Is.SameAs(fileStream));
        }
    }
}
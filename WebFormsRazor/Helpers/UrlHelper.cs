using System.Web;

namespace WebFormsRazor.Helpers
{
    public class UrlHelper
    {

        // Private Variables
        private readonly HttpContextBase _context;


        // Constructor
        public UrlHelper(HttpContextBase webPage)
        {
            _context = webPage;
        }


        // Public Methods
        public string ResolveUrl(string relativeUrl)
        {
            if (relativeUrl.StartsWith("/")) // goes back to domain root by default, nothing to resolve
                return relativeUrl;

            var currentPath = GetCurrentUrlDirectory();

            var url = relativeUrl.StartsWith("~")
                ? relativeUrl.Replace("~", _context.Request.ApplicationPath)
                : (currentPath + relativeUrl);

            return url.Replace("//", "/");
        }

        public string ResolveUrl(string relativeUrl, params object[] args)
        {
            return ResolveUrl(string.Format(relativeUrl, args));
        }


        // Private Methods
        private string GetCurrentUrlDirectory()
        {
            var currentPath = _context.Request.AppRelativeCurrentExecutionFilePath;

            currentPath = currentPath
                .Remove(currentPath.LastIndexOf('/') + 1)
                .Replace("~", ""); ;

            return (!currentPath.EndsWith("/"))
                ? currentPath + "/"
                : currentPath;
        }


    }
}

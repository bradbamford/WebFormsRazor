using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Compilation;
using System.Web.WebPages;
using WebFormsRazor.Helpers;

namespace WebFormsRazor
{
    public class RazorEngine
    {

        // Constructor
        public RazorEngine(string razorScriptFile)
        {
            RazorScriptFile = razorScriptFile;

            try
            {
                InitWebpage();
            }
            catch (HttpParseException ex)
            {
                throw;
            }
            catch (HttpCompileException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // Properties
        protected string RazorScriptFile { get; set; }

        protected HttpContextBase HttpContext => new HttpContextWrapper(System.Web.HttpContext.Current);

        public RazorWebPage Webpage { get; set; }



        // public Methods
        public Type RequestedModelType()
        {
            if (Webpage == null) return null;

            var type = Webpage.GetType();
            return type.BaseType.IsGenericType ? type.BaseType.GetGenericArguments()[0] : null;
        }

        public void Render<T>(TextWriter writer, T model)
        {
            if (Webpage is RazorWebPage)
                ((RazorWebPage<T>)Webpage).Model = model;

            Webpage.ExecutePageHierarchy(new WebPageContext(HttpContext, Webpage, null), writer, Webpage);
        }
        
        public void Render(TextWriter writer)
        {
            Webpage.ExecutePageHierarchy(new WebPageContext(HttpContext, Webpage, null), writer, Webpage);
        }



        // Private Methods
        private object CreateWebPageInstance()
        {
            var compiledType = BuildManager.GetCompiledType(RazorScriptFile);

            return compiledType != null
              ? RuntimeHelpers.GetObjectValue(Activator.CreateInstance(compiledType))
              : null;

        }

        private void InitHelpers(RazorWebPage webPage)
        {
            webPage.Html = new HtmlHelper(webPage.Context);
            webPage.Url = new UrlHelper(webPage.Context);
        }

        private void InitWebpage()
        {
            if (string.IsNullOrEmpty(RazorScriptFile))
                return;
            var objectValue = RuntimeHelpers.GetObjectValue(CreateWebPageInstance());
            if (objectValue == null)
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The webpage found at '{0}' was not created.", RazorScriptFile));

            Webpage = objectValue as RazorWebPage;

            if (Webpage == null)
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The webpage at '{0}' must derive from RazorEnabledPage.", RazorScriptFile));
            Webpage.Context = HttpContext;
            Webpage.VirtualPath = VirtualPathUtility.GetDirectory(RazorScriptFile);
            InitHelpers(Webpage);
        }
    }
}

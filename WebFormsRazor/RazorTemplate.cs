using System.IO;

namespace WebFormsRazor
{
    public static class RazorTemplate
    {

        // Render With model
        public static string Render<T>(T model, string razorFileVirtualPath)
        {
            var razorEngine = GetRazorEngine(razorFileVirtualPath);
            var writer      = new StringWriter();
            razorEngine.Render(writer, model);
            return System.Net.WebUtility.HtmlDecode(writer.ToString());
        }

        // Render No Model
        public static string Render(string razorFileVirtualPath)
        {
            var razorEngine = GetRazorEngine(razorFileVirtualPath);
            var writer      = new StringWriter();
            razorEngine.Render(writer);
            return System.Net.WebUtility.HtmlDecode(writer.ToString());
        }


        // Render and Add to a Literal Control
        public static void Render<T>(T model, string razorFileVirtualPath, System.Web.UI.WebControls.Literal lit)
        {
            lit.Text = Render(model, razorFileVirtualPath);
        }

        public static void Render(string razorFileVirtualPath, System.Web.UI.WebControls.Literal lit)
        {
            lit.Text = Render(razorFileVirtualPath);
        }


        // Render and Add to a Placeholder Control
        public static void Render<T>(T model, string razorFileVirtualPath, System.Web.UI.WebControls.PlaceHolder ph)
        {
            ph.Controls.Add(new System.Web.UI.LiteralControl(Render(model, razorFileVirtualPath)));
        }

        public static void Render(string razorFileVirtualPath, System.Web.UI.WebControls.PlaceHolder ph)
        {
            ph.Controls.Add(new System.Web.UI.LiteralControl(Render(razorFileVirtualPath)));
        }




        // Private RazorEngine Initializer 
        private static RazorEngine GetRazorEngine(string razorFileVirtualPath)
        {
            return new RazorEngine(razorFileVirtualPath);
        }

        

    }

}

using System.Web;

namespace WebFormsRazor.Helpers
{
    public class HtmlHelper
    {
        
        // Private Variables
        private HttpContextBase _context;


        // Constructor
        public HtmlHelper(HttpContextBase context)
        {
            _context = context;
        }


        // Public Methods
        public HtmlString Raw(string text)
        {
            return new HtmlString(text);
        }
    }
}

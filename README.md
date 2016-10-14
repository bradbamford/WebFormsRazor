# WebFormsRazor

Allows the use of Razor in WebForms, for senarios where you want to simply generate clean HTML without the need for server controls.


How to Use:
---------------------
Rendering the Template is as simple as calling RazorHelper.Render() passing in your model and path to the razor file.

```cs
// Get the generated Html
var html = RazorTemplate.Render(myModel, "~/template.cshtml");

// add the html to a control.
Literal1.Text = html;
```

The only hurdle is razor files must inherit from RazorWebPage.
```cs
@inherits WebFormsRazor.RazorWebPage<myModel>
```

Notes
-----------------
The idea for this came directly from the way [DotNetNuke](http://www.dnnsoftware.com) is doing the exact same thing except inside it's own framework using [DotNetNuke.Web.Razor](https://github.com/dnnsoftware/Dnn.Platform/blob/fd225b8de07042837f7473cd49fba13de42a3cc0/DNN%20Platform/DotNetNuke.Web.Razor/RazorEngine.cs).
I wanted to be able to do the same thing in standard ASP.NET WebForms ASPX pages. However, I could never find a way to render html 
using just a razor file and a model for model binding. So, this is my attempt to extract out how DNN was doing it, but for use in a standard ASPX page.

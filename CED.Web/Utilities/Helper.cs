using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CED.Web.Utilities;

public class Helper
{
 

    public static string RenderRazorViewToString(Controller? controller, string viewName, object? model = null)
    {
        if (controller is null) return string.Empty;
        controller.ViewData.Model = model;
        using (var sw = new StringWriter())
        {
            IViewEngine? viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            if (viewEngine is null) return string.Empty;
            ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);
            if (viewResult is null || viewResult.View is null) return string.Empty;

            ViewContext viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                sw,
                new HtmlHelperOptions()
            );
            viewResult.View.RenderAsync(viewContext);
            return sw.GetStringBuilder().ToString();
        }
    }
   
    public static async Task<string> SaveFiles(IFormFile formFile, string wwwRootPath)
    {
        if (formFile != null && formFile.Length > 0)
        {
            string fileName = formFile.FileName;
            string path = Path.Combine(wwwRootPath + "/avatar/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
                
            }
            return fileName;
        }

        return string.Empty;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class NoDirectAccessAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (
            filterContext.HttpContext.Request.GetTypedHeaders().Referer == null ||
             filterContext.HttpContext.Request.GetTypedHeaders().Host.Host.ToString()
             != filterContext.HttpContext.Request.GetTypedHeaders().Referer?.Host.ToString()
             )
        {
            filterContext.HttpContext.Response.Redirect("/");
        }
    }
}

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace PicoBoards.Web
{
    public static class RazorPageExtensions
    {
        public static string GetControllerName(this RazorPage instance)
            => instance.GetControllerActionDescriptor().ControllerName;

        public static string GetActionName(this RazorPage instance)
            => instance.GetControllerActionDescriptor().ActionName;

        public static string GetId(this RazorPage instance)
            => instance.GetControllerActionDescriptor().Id;

        public static ControllerActionDescriptor GetControllerActionDescriptor(this RazorPage instance)
            => (ControllerActionDescriptor) instance.ViewContext.ActionDescriptor;
    }
}
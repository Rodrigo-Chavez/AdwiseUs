using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

public class CultureAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        string cultureName = (string)filterContext.RouteData.Values["culture"] ?? "es-ES";

        // Validar la cultura
        cultureName = CultureHelper.GetImplementedCulture(cultureName);

        // Establecer la cultura
        CultureInfo cultureInfo = new CultureInfo(cultureName);
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;

        base.OnActionExecuting(filterContext);
    }
}

public static class CultureHelper
{
    // Lista de culturas implementadas
    private static readonly List<string> _cultures = new List<string> { "es-ES", "en-US" };

    public static string GetImplementedCulture(string cultureName)
    {
        // Si la cultura no está implementada, regresar la cultura por defecto (es-ES)
        if (!_cultures.Contains(cultureName))
        {
            return "es-ES";
        }
        return cultureName;
    }
    public static string ToggleCulture(string currentCulture)
    {
        return currentCulture == "es-ES" ? "en-US" : "es-ES";
    }
}

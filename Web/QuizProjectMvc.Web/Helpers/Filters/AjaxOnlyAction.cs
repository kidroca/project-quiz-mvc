namespace QuizProjectMvc.Web.Helpers.Filters
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxOnlyAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 404;
                filterContext.Result = new HttpNotFoundResult();
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}

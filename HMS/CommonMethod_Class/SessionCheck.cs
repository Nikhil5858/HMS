using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HMS.CommonMethod_Class
{
    public class SessionCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isAnonymous = context.ActionDescriptor.EndpointMetadata
                                    .OfType<AllowAnonymousAttribute>().Any();

            if (isAnonymous)
            {
                return;
            }


            base.OnActionExecuting(context);
        }
    }
}
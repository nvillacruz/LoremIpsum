using LoremIpsum.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// Action filter to check the model state before the controller action is invoked
    /// Don't forget to add [ValidateModel] above the Controller method
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var result = new ApiResponse
                {
                    ErrorMessage = actionContext.ModelState.AggregateErrors()
                };

                actionContext.Result = new OkObjectResult(result);
            }

        }
    }
}

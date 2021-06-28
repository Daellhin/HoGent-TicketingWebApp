using _2021_dotnet_g_04.Models.Domain.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace _2021_dotnet_g_04.Filters
{
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class KlantFilter: ActionFilterAttribute
    {
        private readonly IKlantRepository _klantRepository;

        public KlantFilter(IKlantRepository klantRepository)
        {
            _klantRepository = klantRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["klant"] = context.HttpContext.User.Identity.IsAuthenticated ? _klantRepository.GetKlantFromUsername(context.HttpContext.User.Identity.Name) : null;
            base.OnActionExecuting(context);
        }
    }
}

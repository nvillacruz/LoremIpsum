using LoremIpsum.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoremIpsum.Web.Server
{

    /// <summary>
    /// Manages the Web API calls for miscellaneous (calls that does not use EF Core)
    /// </summary>
    [Produces("application/json")]
    public class MiscellaneousController
    {
        public MiscellaneousController()
        {

        }

        [Route(ApiRoutes.GetLocalizedStrings)]
        public async Task<CommonStringsModel> GetLocalizedStringsAsync(LanguageType language)
        {

            return await Task.FromResult(new CommonStringsModel());
        }
    }
}

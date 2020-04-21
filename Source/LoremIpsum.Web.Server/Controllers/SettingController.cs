using LoremIpsum.Core;
using LoremIpsum.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [AuthorizeToken]
    public class SettingController
    {
        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected ApplicationDbContext mContext;

        #region Default Constructor
        public SettingController(ApplicationDbContext context)
        {
            mContext = context;
        }
        #endregion

        [Route(ApiRoutes.GetEnterpriseSetting)]
        public async Task<ApiResponse<EnterpriseSettingResultApiModel>> GetEnterpriseSettingsAsync()
        {
            var setting = await mContext.EnterpriseSettings.Select(x => new EnterpriseSettingResultApiModel
            {
                CompanyName = x.CompanyName

            }).FirstOrDefaultAsync();

            return new ApiResponse<EnterpriseSettingResultApiModel>
            {
                Response = setting
            };
        }

        [Route(ApiRoutes.UpdateEnterpriseSetting)]
        [HttpPost]
        public async Task<ApiResponse> UpdateEnterpriseSettingsAsync([FromBody]UpdateEnterpriseSettingsApiModel enterpriseSetting)
        {
            var setting = await mContext.EnterpriseSettings.FirstOrDefaultAsync();

            setting.CompanyName = enterpriseSetting.CompanyName;

            try
            {
                await mContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ApiResponse.Failed(ex.Message);
            }

            return ApiResponse.Success;



        }
    }
}

namespace PugnaFighting.Web.Areas.Administration.Controllers
{
    using PugnaFighting.Common;
    using PugnaFighting.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}

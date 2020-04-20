namespace PugnaFighting.Web.ViewModels.Managers
{
    using System.Collections.Generic;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Web.ViewModels.Fighters;

    public class AllManagersViewModel
    {
        public IEnumerable<ManagerViewModel> ManagerViewModels { get; set; }

        public IEnumerable<FightersDropDownViewModel> Fighters { get; set; }

        public ApplicationUser User { get; set; }
    }
}

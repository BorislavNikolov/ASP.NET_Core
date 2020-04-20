namespace PugnaFighting.Web.ViewModels.Managers
{
    using System.Collections.Generic;

    public class AllManagersViewModel
    {
        public IEnumerable<ManagerViewModel> ManagerViewModels { get; set; }

        public bool HasFighterWithoutManager { get; set; }
    }
}

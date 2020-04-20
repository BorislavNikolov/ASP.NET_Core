namespace PugnaFighting.Web.ViewModels.Fighters
{
    using System.Collections.Generic;

    using PugnaFighting.Data.Models;

    public class AllFightersViewModel
    {
       public IEnumerable<FighterViewModel> FighterViewModels { get; set; }

       public ApplicationUser User { get; set; }
    }
}

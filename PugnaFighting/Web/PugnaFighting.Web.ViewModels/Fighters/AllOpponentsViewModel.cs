namespace PugnaFighting.Web.ViewModels.Fighters
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllOpponentsViewModel
    {
        public IEnumerable<OpponentViewModel> Opponents { get; set; }

        public IEnumerable<FightersDropDownViewModel> Fighters { get; set; }

        public IEnumerable<FightersDropDownViewModel> OpponentsDropDown { get; set; }

        [Range(1, int.MaxValue)]
        public int FighterId { get; set; }

        [Range(1, int.MaxValue)]
        public int OpponentId { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}

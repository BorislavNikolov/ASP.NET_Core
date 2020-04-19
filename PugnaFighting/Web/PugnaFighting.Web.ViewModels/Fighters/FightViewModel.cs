namespace PugnaFighting.Web.ViewModels.Fighters
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class FightViewModel
    {
        public int FighterId { get; set; }

        public FighterFightViewModel Fighter { get; set; }

        public int OpponentId { get; set; }

        public FighterFightViewModel Opponent { get; set; }
    }
}

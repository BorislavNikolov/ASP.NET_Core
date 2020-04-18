namespace PugnaFighting.Web.ViewModels.Fighters
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class FightViewModel : IMapFrom<Fighter>
    {
        public Fighter Fighter { get; set; }

        public Fighter Opponent { get; set; }
    }
}

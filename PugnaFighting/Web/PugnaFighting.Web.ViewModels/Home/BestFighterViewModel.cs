namespace PugnaFighting.Web.ViewModels.Home
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class BestFighterViewModel : IMapFrom<Fighter>
    {
        public Biography Biography { get; set; }

        public Category Category { get; set; }

        public string UserUsername { get; set; }
    }
}

namespace PugnaFighting.Web.ViewModels.Skills
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class TrainViewModel : IMapFrom<Skill>
    {
        public int Striking { get; set; }

        public int Grappling { get; set; }

        public int Wrestling { get; set; }

        public int Health { get; set; }

        public int Strenght { get; set; }

        public int Stamina { get; set; }
    }
}

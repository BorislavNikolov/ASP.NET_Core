namespace PugnaFighting.Web.ViewModels.Fighters
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class DetailsFighterViewModel : IMapFrom<Fighter>
    {
        public int Id { get; set; }

        public int FansCount { get; set; }

        public Biography Biography { get; set; }

        public Category Category { get; set; }

        public Skill Skill { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual Coach Coach { get; set; }

        public virtual Cutman Cutman { get; set; }
    }
}

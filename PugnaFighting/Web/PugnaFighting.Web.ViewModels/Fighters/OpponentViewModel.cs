namespace PugnaFighting.Web.ViewModels.Fighters
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class OpponentViewModel : IMapFrom<Fighter>
    {
        public int Id { get; set; }

        public Biography Biography { get; set; }

        public Category Category { get; set; }

        public Organization Organization { get; set; }
    }
}

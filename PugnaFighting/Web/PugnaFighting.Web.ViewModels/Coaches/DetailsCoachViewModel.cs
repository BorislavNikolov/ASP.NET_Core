namespace PugnaFighting.Web.ViewModels.Coaches
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class DetailsCoachViewModel : IMapFrom<Coach>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BornCountry { get; set; }

        public int Age { get; set; }

        public string PictureUrl { get; set; }

        public int Price { get; set; }

        public int SkillBonus { get; set; }
    }
}

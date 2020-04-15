namespace PugnaFighting.Web.ViewModels.Managers
{
    using System.Collections.Generic;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;
    using PugnaFighting.Web.ViewModels.Fighters;

    public class DetailsManagerViewModel : IMapFrom<Manager>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BornCountry { get; set; }

        public int Age { get; set; }

        public string PictureUrl { get; set; }

        public int Price { get; set; }

        public int MoneyPerFight { get; set; }

        public int FansCount { get; set; }

        public IEnumerable<FightersDropDownViewModel> Fighters { get; set; }
    }
}

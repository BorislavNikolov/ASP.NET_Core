﻿namespace PugnaFighting.Web.ViewModels.Fighters
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class FightersDropDownViewModel : IMapFrom<Fighter>
    {
        public int Id { get; set; }

        public Biography Biography { get; set; }
    }
}

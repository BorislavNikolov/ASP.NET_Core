namespace PugnaFighting.Web.ViewModels.Fighters
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

namespace PugnaFighting.Data.Models
{
    using PugnaFighting.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string WeightLimit { get; set; }
    }
}

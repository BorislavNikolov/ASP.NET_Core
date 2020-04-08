namespace PugnaFighting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Organization : BaseDeletableModel<int>
    {
        public Organization()
        {
            this.Fighters = new List<Fighter>();
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Location { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        [Required]
        public int FansCount { get; set; }

        [Required]
        public int InstantCash { get; set; }

        [Required]
        public int MoneyPerFight { get; set; }

        public virtual ICollection<Fighter> Fighters { get; set; }
    }
}

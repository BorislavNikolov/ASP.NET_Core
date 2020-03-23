namespace PugnaFighting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Team : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Location { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        public virtual ICollection<Coach> Coaches => new List<Coach>();
    }
}

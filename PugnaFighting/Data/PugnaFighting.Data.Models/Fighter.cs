namespace PugnaFighting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Fighter : BaseDeletableModel<int>
    {
        [Required]
        public int FansCount { get; set; }

        [Required]
        public int MoneyPerFight { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int BiographyId { get; set; }

        [Required]
        public virtual Biography Biography { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public int SkillId { get; set; }

        [Required]
        public virtual Skill Skill { get; set; }

        public int? OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        public int? ManagerId { get; set; }

        public virtual Manager Manager { get; set; }

        public int? CoachId { get; set; }

        public virtual Coach Coach { get; set; }

        public int? CutmanId { get; set; }

        public virtual Cutman Cutman { get; set; }
    }
}

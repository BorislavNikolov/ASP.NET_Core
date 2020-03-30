namespace PugnaFighting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Fighter : BaseDeletableModel<int>
    {
        [Required]
        public int FansCount { get; set; }

        [Required]
        public virtual int PersonalInfoId { get; set; }

        [Required]
        public virtual PersonalInfo PersonalInfo { get; set; }

        [Required]
        public virtual int SkillId { get; set; }

        [Required]
        public virtual Skill Skill { get; set; }

        public virtual int ManagerId { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual int CoachId { get; set; }

        public virtual Coach Coach { get; set; }

        public virtual int CutmanId { get; set; }

        public virtual Cutman Cutman { get; set; }
    }
}

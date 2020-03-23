namespace PugnaFighting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Fighter : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nationality { get; set; }

        [Required]
        [Range(18, 50)]
        public int Age { get; set; }

        [Required]
        [Range(0, 100)]
        public int Striking { get; set; }

        [Required]
        [Range(0, 100)]
        public int Grappling { get; set; }

        [Required]
        [Range(0, 100)]
        public int Wrestling { get; set; }

        [Required]
        [Range(0, 100)]
        public int Health { get; set; }

        [Required]
        [Range(0, 100)]
        public int Strenght { get; set; }

        [Required]
        [Range(0, 100)]
        public int Stamina { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public int FansCount { get; set; }

        public virtual int ManagerId { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual int CoachId { get; set; }

        public virtual Coach Coach { get; set; }

        public virtual int CutmanId { get; set; }

        public virtual Cutman Cutman { get; set; }
    }
}

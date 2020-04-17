namespace PugnaFighting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Record : BaseDeletableModel<int>
    {
        public Record()
        {
            this.Wins = 0;
            this.Draws = 0;
            this.Losses = 0;
            this.Fights = new List<Fight>();
        }

        [Required]
        public int Wins { get; set; }

        [Required]
        public int Draws { get; set; }

        [Required]
        public int Losses { get; set; }

        [Required]
        public IEnumerable<Fight> Fights { get; set; }
    }
}

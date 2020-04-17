namespace PugnaFighting.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Common.Models;

    public class Fight : BaseDeletableModel<int>
    {
        [Required]
        public string Result { get; set; }

        [Required]
        public string OpponentName { get; set; }

        [Required]
        public string Method { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}

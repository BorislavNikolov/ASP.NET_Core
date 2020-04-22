namespace PugnaFighting.Web.ViewModels.Fights
{
    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class FightReportViewModel : IMapFrom<Fight>
    {
        public string Result { get; set; }

        public string OpponentName { get; set; }

        public string Method { get; set; }

        public int FinalRound { get; set; }
    }
}

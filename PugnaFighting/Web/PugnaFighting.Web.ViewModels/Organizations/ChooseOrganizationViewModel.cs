namespace PugnaFighting.Web.ViewModels.Organizations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PugnaFighting.Data.Models;
    using PugnaFighting.Services.Mapping;

    public class ChooseOrganizationViewModel
    {
        public IEnumerable<OrganizationViewModel> Organizations { get; set; }

        public IEnumerable<OrganizationDropDownViewModel> OrganizationsDropDown { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Organization")]
        public int OrganizationId { get; set; }
    }
}

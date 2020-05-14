using Ferroviario.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Models
{
    public class ShiftViewModel : ShiftEntity
    {
        [Required(ErrorMessage = "Please Select a driver")]
        [Display(Name = "Driver")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "You must select a driver")]
        public string User { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Service")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a service.")]
        public int Service { get; set; }

        public IEnumerable<SelectListItem> Drivers { get; set; }

        public IEnumerable<SelectListItem> Services { get; set; }
    }
}

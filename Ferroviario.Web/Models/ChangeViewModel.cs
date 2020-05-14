using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Models
{
    public class ChangeViewModel
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "First Driver")]
        public int FirstDriverId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Second Driver")]
        public int SecondDriverId { get; set; }


    }
}

using Ferroviario.Web.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Data.Entities
{
    public class ServiceEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MinLength(3, ErrorMessage = "The {0} field can not have less than {1} characters.")]
        [Display(Name = "Code Service")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [Display(Name = "Initial Hour")]
        public TimeSpan InitialHour { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Initial Station")]
        public string InitialStation { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [Display(Name = "Final Hour")]
        public TimeSpan FinalHour { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Final Station")]
        public string FinalStation { get; set; }

        public ServiceDetailEntity ServiceDetail { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]        
        public TimeSpan Duration => FinalHour - InitialHour;

        public ICollection<ShiftEntity> Shifts { get; set; }

    }
}

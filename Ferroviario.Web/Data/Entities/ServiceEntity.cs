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
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan InitialHour { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string InitialStation { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan FinalHour { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FinalStation { get; set; }

        public ServiceDetailEntity ServiceDetail { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan Duration => FinalHour - InitialHour;

        public List<ShiftEntity> Users { get; set; }
    }
}

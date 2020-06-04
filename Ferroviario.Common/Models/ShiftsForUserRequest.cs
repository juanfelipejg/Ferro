using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class ShiftsForUserRequest
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid UserId { get; set; }

        public int CurrentHour { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}

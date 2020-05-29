using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class RequestRequest
    {

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public DateTime InitialDate { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public DateTime FinishDate { get; set; }

        public string Description { get; set; }

        [Required]
        public string CultureInfo { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid UserId { get; set; }

    }
}

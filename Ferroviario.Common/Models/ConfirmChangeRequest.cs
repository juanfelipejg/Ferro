using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class ConfirmChangeRequest
    {
        [Required]
        public int ChangeId { get; set; }

        [Required]
        public int FirstServiceId { get; set; }

        [Required]
        public int SecondServiceId { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}

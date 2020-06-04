using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ferroviario.Common.Models
{
    public class ReportRequest
    {
        public string Source { get; set; }

        public double SourceLatitude { get; set; }

        public double SourceLongitude { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Description { get; set; }        

        [Required]
        public string CultureInfo { get; set; }
    }
}

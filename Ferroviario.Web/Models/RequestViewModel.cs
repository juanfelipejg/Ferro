using Ferroviario.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Models
{
    public class RequestViewModel : RequestEntity
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Request Types")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a request type.")]
        public int Type { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        public string UserId { get; set; }
    }
}

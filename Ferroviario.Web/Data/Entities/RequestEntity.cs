using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Data.Entities
{
    public class RequestEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public RequestTypeEntity Type { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd }", ApplyFormatInEditMode = false)]
        public DateTime InitialDate { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd }", ApplyFormatInEditMode = false)]
        public DateTime FinishDate { get; set; }

        public string Description { get; set; }

        public string State { get; set; }

        public string Comment { get; set; }
    }
}

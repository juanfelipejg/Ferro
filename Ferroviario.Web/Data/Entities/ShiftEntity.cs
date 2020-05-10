using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Data.Entities
{
    public class ShiftEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd }", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }
    }
}

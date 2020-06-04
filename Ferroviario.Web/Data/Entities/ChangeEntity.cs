using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Data.Entities
{
    public class ChangeEntity
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Date  { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime DateLocal => Date.ToLocalTime();

        [Display(Name = "Sender Driver")]
        public UserEntity FirstDriver { get; set; }

        [Display(Name = "Service")]
        public ShiftEntity FirstDriverService { get; set; }

        [Display(Name = "Receiver Driver")]
        public UserEntity SecondDriver { get; set; }

        [Display(Name = "Service")]
        public ShiftEntity SecondDriverService { get; set; }

        public string State { get; set; }

    }
}

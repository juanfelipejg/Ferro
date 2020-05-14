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
        public DateTime Date => DateTime.Today.AddDays(1).ToUniversalTime();

        public DateTime DateLocal => Date.ToLocalTime();

        public UserEntity FirstDriver { get; set; }

        public ServiceEntity FirstDriverService { get; set; }

        public UserEntity SecondDriver { get; set; }

        public ServiceEntity SecondDriverService { get; set; }

    }
}

using Ferroviario.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Models
{
    public class ChangeViewModel : ChangeEntity
    {
        public string FirstDriverId { get; set; }

        public int FirstDriverServiceId { get; set; }

        public string SecondDriverId { get; set; }

        public int SecondDriverServiceId { get; set; }        

    }
}

using Ferroviario.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Models
{
    public class ServiceDetailViewModel:  ServiceDetailEntity
    {
        public int ServiceId { get; set; }
    }
}

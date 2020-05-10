using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Data.Entities
{
    public class ChangeEntity
    {
        public int Id { get; set; }

        public DateTime Date;

        public string State { get; set; }

        public ServiceEntity FirstService { get; set; }

        public ServiceEntity SecondService { get; set; }
    }
}

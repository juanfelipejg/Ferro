using Ferroviario.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Models
{
    public class ShiftModel : ShiftEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public int ServiceId { get; set; }

        public int Id2 { get; set; }

        public string UserId2 { get; set; }

        public int ServiceId2 { get; set; }
    }
}

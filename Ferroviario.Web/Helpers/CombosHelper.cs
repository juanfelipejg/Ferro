using Ferroviario.Common.Enums;
using Ferroviario.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Ferroviario.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboDrivers()
        {
            List<SelectListItem> list = _context.Users.Where(u=>u.UserType == UserType.User).
                Select(t => new SelectListItem
            {
                Text = t.FullName,
                Value = $"{t.Id}"
            })
            .OrderBy(t => t.Text)
            .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a driver]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboServices()
        {
            List<SelectListItem> list = _context.Services.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
            .OrderBy(t => t.Text)
            .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a service]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboTypes()
        {
            List<SelectListItem> list = _context.RequestTypes.Select(t => new SelectListItem
            {
                Text = t.Type,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a request type]",
                Value = "0"
            });

            return list;
        }


    }

}

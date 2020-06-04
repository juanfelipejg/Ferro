using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Helpers
{
    public class ChangeHelper : IChangeHelper
    {
        private readonly DataContext _context;

        public ChangeHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<int> CheckChanges(UserEntity user)
        {
            int count = 0; 
            DateTime tomorrow = DateTime.Today.AddDays(1).ToUniversalTime();

            List<ChangeEntity> changes = await _context.Changes.Include(c => c.FirstDriver).
            Include(c => c.SecondDriver).
            Where(c => c.Date.Day == tomorrow.Day && c.State == "Approved").ToListAsync();

            foreach (ChangeEntity change in changes)
            {
                if (change.FirstDriver == user || change.SecondDriver == user)
                {
                    count += 1;
                }
            };

            return count;
        }

        public async Task<bool> CheckHours (UserEntity userEntity, ShiftEntity shiftEntity)
        {
            DateTime today = DateTime.Today.ToUniversalTime();

            DateTime tomorrow = DateTime.Today.AddDays(1).ToUniversalTime();

            ShiftEntity todayShift = await _context.Shifts.Include(s=>s.Service).
            Include(s => s.User).Where(s => s.User == userEntity && s.Date == today).
            FirstOrDefaultAsync();

            TimeSpan finalToday = todayShift.Service.FinalHour;

            TimeSpan startTomorrow = shiftEntity.Service.InitialHour;

            DateTime dateToday = new DateTime(today.Year, today.Month, today.Day, finalToday.Hours, finalToday.Minutes, finalToday.Seconds);

            DateTime dateTomorrow = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, startTomorrow.Hours, startTomorrow.Minutes, startTomorrow.Seconds);

            if ((dateTomorrow-dateToday).TotalHours < 10)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}

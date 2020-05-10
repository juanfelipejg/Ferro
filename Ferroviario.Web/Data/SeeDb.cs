using Ferroviario.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRequestTypeAsync();
            await CheckRequestAsync();
            await CheckServiceAsync();
        }

        private async Task CheckRequestTypeAsync()
        {
            if (!_context.RequestTypes.Any())
            {
                AddRequestType("Holidays");
                AddRequestType("License");
                AddRequestType("Free day");
                AddRequestType("Academic Permission");
                await _context.SaveChangesAsync();

            }
        }

        private void AddRequestType(string name)
        {
            _context.RequestTypes.Add(new RequestTypeEntity { Type = name });

        }

        private async Task CheckRequestAsync()
        {
            if (!_context.Requests.Any())
            {
                DateTime initialDate = DateTime.Today.AddDays(15).ToUniversalTime();
                DateTime finalDate = DateTime.Today.AddMonths(30).ToUniversalTime();

                _context.Requests.Add(new RequestEntity
                {
                    Type = _context.RequestTypes.FirstOrDefault(t => t.Type == "Holidays"),
                    State = "Aprobado",
                    InitialDate = initialDate,
                    FinishDate = finalDate,
                    Description = "I request holidays, because I have a trip",
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckServiceAsync()
        {
            if (!_context.Services.Any())
            {
                _context.Services.Add(new ServiceEntity
                {
                    Name = "929LV",
                    InitialHour = new TimeSpan(8, 30, 0),
                    InitialStation = "La Estrella",
                    FinalHour = new TimeSpan(12, 30, 0),
                    FinalStation = "La Estrella",
                    ServiceDetail = (new ServiceDetailEntity
                    {
                        Description = "La Estrella 8:35 Tren 50 - Niquia 9:15 Tren 51 \r\n" +
                                   "La Estrella 9:55 Tren 54 - Niquia 10:40 Tren 56 \r\n" +
                                   "La Estrella 11:22 Tren 58 - Niquia Descanso"
                    })
                });
            }
            await _context.SaveChangesAsync();
        }

    }
}

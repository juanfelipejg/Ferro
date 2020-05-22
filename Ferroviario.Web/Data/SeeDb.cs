using Ferroviario.Common.Enums;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public IUserHelper _userHelper { get; }

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Juan", "Jaramillo", "juanfelipejg@gmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Admin);
            await CheckUserAsync("2020", "Juan", "Jaramillo", "juanfelipejg@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckUserAsync("2030", "Diego", "Renal", "diego@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckUserAsync("2040", "Camilo", "Betancur", "camilo@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckUserAsync("2050", "Jose", "Acosta", "jose@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckUserAsync("2060", "Manuela", "Noreña", "manuela@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckRequestTypeAsync();
            await CheckRequestAsync();
            await CheckServiceAsync();
            await CheckShiftAsync();
        }

        private async Task CheckShiftAsync()
        {
            DateTime DateToday = DateTime.Today.ToUniversalTime();
            DateTime DateTomorrow = DateTime.Today.AddDays(1).ToUniversalTime();
            if (!_context.Shifts.Any())
            {
                _context.Shifts.Add(new ShiftEntity
                {
                    User = _context.Users.FirstOrDefault(u => u.Document == "2020"),
                    Service = _context.Services.FirstOrDefault(s => s.Id == 1),
                    Date = DateToday
                });
                _context.Shifts.Add(new ShiftEntity
                {
                    User = _context.Users.FirstOrDefault(u => u.Document == "2030"),
                    Service = _context.Services.FirstOrDefault(s => s.Id == 2),
                    Date = DateToday
                });
                _context.Shifts.Add(new ShiftEntity
                {
                    User = _context.Users.FirstOrDefault(u => u.Document == "2040"),
                    Service = _context.Services.FirstOrDefault(s => s.Id == 3),
                    Date = DateToday
                });
                _context.Shifts.Add(new ShiftEntity
                {
                    User = _context.Users.FirstOrDefault(u => u.Document == "2050"),
                    Service = _context.Services.FirstOrDefault(s => s.Id == 4),
                    Date = DateToday
                });
                _context.Shifts.Add(new ShiftEntity
                {
                    User = _context.Users.FirstOrDefault(u => u.Document == "2060"),
                    Service = _context.Services.FirstOrDefault(s => s.Id == 5),
                    Date = DateToday
                });
                _context.Shifts.Add(new ShiftEntity
                {
                    User = _context.Users.FirstOrDefault(u=>u.Document =="2020"),
                    Service = _context.Services.FirstOrDefault(s=>s.Id == 1),
                    Date = DateTomorrow
                });
                _context.Shifts.Add(new ShiftEntity
                {
                    User = _context.Users.FirstOrDefault(u => u.Document == "2030"),
                    Service = _context.Services.FirstOrDefault(s => s.Id == 2),
                    Date = DateTomorrow
                });
                _context.Shifts.Add(new ShiftEntity
                {
                    User = _context.Users.FirstOrDefault(u => u.Document == "2040"),
                    Service = _context.Services.FirstOrDefault(s => s.Id == 3),
                    Date = DateTomorrow
                });
                _context.Shifts.Add(new ShiftEntity
                {
                    User = _context.Users.FirstOrDefault(u => u.Document == "2050"),
                    Service = _context.Services.FirstOrDefault(s => s.Id == 4),
                    Date = DateTomorrow
                });
                _context.Shifts.Add(new ShiftEntity
                {
                    User = _context.Users.FirstOrDefault(u => u.Document == "2060"),
                    Service = _context.Services.FirstOrDefault(s => s.Id == 5),
                    Date = DateTomorrow
                });

                await _context.SaveChangesAsync();
            }
        }


        private async Task<UserEntity> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {

            UserEntity user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;

        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());

        }

        private async Task CheckRequestTypeAsync()
        {
            if (!_context.RequestTypes.Any())
            {
                AddRequestType("Holidays");
                AddRequestType("License");
                AddRequestType("Free day");
                AddRequestType("Academic Permission");
                AddRequestType("Others");
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
                    InitialDate = initialDate,
                    FinishDate = finalDate,
                    Description = "I request holidays, because I have a trip",
                    State = "Aprobado",
                    User = _context.Users.FirstOrDefault(u=>u.Email == "juanfelipejg@hotmail.com")
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
                _context.Services.Add(new ServiceEntity
                {
                    Name = "930LV",
                    InitialHour = new TimeSpan(12, 30, 0),
                    InitialStation = "Niquia",
                    FinalHour = new TimeSpan(17, 30, 0),
                    FinalStation = "La Estrella",
                    ServiceDetail = (new ServiceDetailEntity
                    {
                        Description = "La Estrella 8:35 Tren 50 - Niquia 9:15 Tren 51 \r\n" +
                   "La Estrella 9:55 Tren 54 - Niquia 10:40 Tren 56 \r\n" +
                   "La Estrella 11:22 Tren 58 - Niquia Descanso"
                    })
                });
                _context.Services.Add(new ServiceEntity
                {
                    Name = "931LV",
                    InitialHour = new TimeSpan(17, 30, 0),
                    InitialStation = "Niquia",
                    FinalHour = new TimeSpan(22, 30, 0),
                    FinalStation = "Niquia",
                    ServiceDetail = (new ServiceDetailEntity
                    {
                        Description = "La Estrella 8:35 Tren 50 - Niquia 9:15 Tren 51 \r\n" +
                   "La Estrella 9:55 Tren 54 - Niquia 10:40 Tren 56 \r\n" +
                   "La Estrella 11:22 Tren 58 - Niquia Descanso"
                    })
                });
                _context.Services.Add(new ServiceEntity
                {
                    Name = "932LV",
                    InitialHour = new TimeSpan(3, 30, 0),
                    InitialStation = "San Antonio",
                    FinalHour = new TimeSpan(8, 30, 0),
                    FinalStation = "La Estrella",
                    ServiceDetail = (new ServiceDetailEntity
                    {
                        Description = "La Estrella 8:35 Tren 50 - Niquia 9:15 Tren 51 \r\n" +
                   "La Estrella 9:55 Tren 54 - Niquia 10:40 Tren 56 \r\n" +
                   "La Estrella 11:22 Tren 58 - Niquia Descanso"
                    })
                });
                _context.Services.Add(new ServiceEntity
                {
                    Name = "933LV",
                    InitialHour = new TimeSpan(6, 30, 0),
                    InitialStation = "San Antonio",
                    FinalHour = new TimeSpan(11, 30, 0),
                    FinalStation = "San Antonio",
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

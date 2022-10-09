using System;
using eStore_Admin.Application.Interfaces.Services;

namespace eStore_Admin.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
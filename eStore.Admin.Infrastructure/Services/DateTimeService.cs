using System;
using eStore.Admin.Application.Interfaces.Services;

namespace eStore.Admin.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}
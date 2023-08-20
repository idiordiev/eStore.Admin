using System;
using eStore.Admin.Application.Interfaces;

namespace eStore.Admin.Application;

public class Clock : IClock
{
    public DateTime UtcNow()
    {
        return DateTime.UtcNow;
    }

    public DateTime Now()
    {
        return DateTime.Now;
    }
}
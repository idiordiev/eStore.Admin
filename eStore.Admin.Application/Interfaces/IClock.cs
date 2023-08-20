using System;

namespace eStore.Admin.Application.Interfaces;

public interface IClock
{
    DateTime UtcNow();
    DateTime Now();
}
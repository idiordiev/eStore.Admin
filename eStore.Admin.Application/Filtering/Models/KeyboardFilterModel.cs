using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Models;

public class KeyboardFilterModel
{
    public ICollection<bool> IsDeletedValues { get; set; }
    public string Name { get; set; }
    public ICollection<string> Manufacturers { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime? CreatedStartDate { get; set; }
    public DateTime? CreatedEndDate { get; set; }
    public ICollection<string> Types { get; set; }
    public ICollection<string> Sizes { get; set; }
    public ICollection<string> ConnectionTypes { get; set; }
    public ICollection<int?> SwitchIds { get; set; }
    public ICollection<string> KeyRollovers { get; set; }
    public ICollection<string> Backlights { get; set; }
    
    public Expression<Func<Keyboard, bool>> CreateExpression()
    {
        var expression = PredicateBuilder.True<Keyboard>();

        if (IsDeletedValues is not null && IsDeletedValues.Any())
        {
            expression = expression.And(k => IsDeletedValues.Contains(k.IsDeleted));
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
            expression = expression.And(k => k.Name.Equals(Name.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }

        if (Manufacturers is not null && Manufacturers.Any())
        {
            expression = expression.And(k =>
                Manufacturers.Any(m => m.Equals(k.Manufacturer, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (MinPrice is not null)
        {
            expression = expression.And(k => k.Price >= MinPrice);
        }

        if (MaxPrice is not null)
        {
            expression = expression.And(k => k.Price <= MaxPrice);
        }

        if (CreatedStartDate is not null)
        {
            expression = expression.And(k => k.Created >= CreatedStartDate);
        }

        if (CreatedEndDate is not null)
        {
            expression = expression.And(k => k.Created <= CreatedEndDate);
        }

        if (ConnectionTypes is not null && ConnectionTypes.Any())
        {
            expression = expression.And(k =>
                ConnectionTypes.Any(ct =>
                    ct.Equals(k.ConnectionType, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (Types is not null && Types.Any())
        {
            expression = expression.And(k =>
                Types.Any(t => t.Equals(k.Type, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (Sizes is not null && Sizes.Any())
        {
            expression = expression.And(k =>
                Sizes.Any(s => s.Equals(k.Size, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (SwitchIds is not null && SwitchIds.Any())
        {
            expression = expression.And(k => SwitchIds.Contains(k.SwitchId));
        }

        if (KeyRollovers is not null && KeyRollovers.Any())
        {
            expression = expression.And(k =>
                KeyRollovers.Any(kr => kr.Equals(k.KeyRollover, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (Backlights is not null && Backlights.Any())
        {
            expression = expression.And(k =>
                Backlights.Any(b => b.Equals(k.Backlight, StringComparison.InvariantCultureIgnoreCase)));
        }

        return expression;
    }
}
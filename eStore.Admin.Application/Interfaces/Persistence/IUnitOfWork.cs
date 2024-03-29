﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace eStore.Admin.Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository CustomerRepository { get; }
    IGamepadRepository GamepadRepository { get; }
    IGoodsRepository GoodsRepository { get; }
    IKeyboardRepository KeyboardRepository { get; }
    IKeyboardSwitchRepository KeyboardSwitchRepository { get; }
    IMousepadRepository MousepadRepository { get; }
    IMouseRepository MouseRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
    IShoppingCartRepository ShoppingCartRepository { get; }

    void Save();
    Task SaveAsync(CancellationToken cancellationToken);
}
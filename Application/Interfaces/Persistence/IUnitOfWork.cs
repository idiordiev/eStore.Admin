using System;
using System.Threading.Tasks;

namespace eStore_Admin.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IGamepadRepository GamepadRepository { get; }
        IGoodsRepository GoodsRepository { get; }
        IKeyboardRepository KeyboardRepository { get; }
        IMousepadRepository MousepadRepository { get; }
        IMouseRepository MouseRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }

        void Save();
        Task SaveAsync();
    }
}
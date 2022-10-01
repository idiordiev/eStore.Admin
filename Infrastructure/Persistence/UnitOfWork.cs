using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Infrastructure.Persistence.Repositories;

namespace eStore_Admin.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        private bool _disposed;
        
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        private ICustomerRepository _customerRepository;
        private IGamepadRepository _gamepadRepository;
        private IGoodsRepository _goodsRepository;
        private IKeyboardRepository _keyboardRepository;
        private IKeyboardSwitchRepository _keyboardSwitchRepository;
        private IMouseRepository _mouseRepository;
        private IMousepadRepository _mousepadRepository;
        private IOrderRepository _orderRepository;
        private IOrderItemRepository _orderItemRepository;
        private IShoppingCartRepository _shoppingCartRepository;

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository is null)
                    _customerRepository = new CustomerRepository(_context);
                return _customerRepository;
            }
        }

        public IGamepadRepository GamepadRepository
        {
            get
            {
                if (_gamepadRepository is null)
                    _gamepadRepository = new GamepadRepository(_context);
                return _gamepadRepository;
            }
        }

        public IGoodsRepository GoodsRepository
        {
            get
            {
                if (_goodsRepository is null)
                    _goodsRepository = new GoodsRepository(_context);
                return _goodsRepository;
            }
        }

        public IKeyboardRepository KeyboardRepository
        {
            get
            {
                if (_keyboardRepository is null)
                    _keyboardRepository = new KeyboardRepository(_context);
                return _keyboardRepository;
            }
        }

        public IKeyboardSwitchRepository KeyboardSwitchRepository {
            get
            {
                if (_keyboardSwitchRepository is null)
                    _keyboardSwitchRepository = new KeyboardSwitchRepository(_context);
                return _keyboardSwitchRepository;
            }
        }

        public IMousepadRepository MousepadRepository
        {
            get
            {
                if (_mousepadRepository is null)
                    _mousepadRepository = new MousepadRepository(_context);
                return _mousepadRepository;
            }
        }

        public IMouseRepository MouseRepository
        {
            get
            {
                if (_mouseRepository is null)
                    _mouseRepository = new MouseRepository(_context);
                return _mouseRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository is null)
                    _orderRepository = new OrderRepository(_context);
                return _orderRepository;
            }
        }

        public IOrderItemRepository OrderItemRepository
        {
            get
            {
                if (_orderItemRepository is null)
                    _orderItemRepository = new OrderItemRepository(_context);
                return _orderItemRepository;
            }
        }

        public IShoppingCartRepository ShoppingCartRepository
        {
            get
            {
                if (_shoppingCartRepository is null)
                    _shoppingCartRepository = new ShoppingCartRepository(_context);
                return _shoppingCartRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
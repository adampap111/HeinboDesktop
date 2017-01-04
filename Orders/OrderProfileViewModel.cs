using Heinbo.Data;
using HeinboDesktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeinboDesktop.Orders
{
    class OrderProfileViewModel : BindableBase
    {
        private IOrderService _repo;
        public OrderProfileViewModel(IOrderService repo)
        {
            _repo = repo;
            CancelCommand = new RelayCommand(OnCancel);

        }
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private OrderProfile _Order;
        public OrderProfile Order
        {
            get { return _Order; }
            set { SetProperty(ref _Order, value); }
        }
        public ObservableCollection<OrderItem> OrderItems
        {
            get;
            set;
        }

        private ObservableCollection<OrderStatus> orderStatus = new ObservableCollection<OrderStatus>();
       
          

        private Order _editingOrder = null;

        public void SerOrder(Order order)
        {
            _editingOrder = order;
          
            Order = new OrderProfile();
          
            CopyCustomer(order, Order);
        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action Done = delegate { };


        private void OnCancel()
        {
            Done();
        }


        private void CopyCustomer(Order source, OrderProfile target)
        {
            target.OrderId = source.OrderId;
            if (EditMode)
            {
                target.OrderDate = source.OrderDate;
                target.User = source.User.LastName + source.User.FirstName;
                target.OrderStatus = source.OrderStatus;
                target.TotalPrice = source.TotalPrice;
              
                OrderItems = new ObservableCollection<OrderItem>(source.OrderItems);

            }
        }
    }
}

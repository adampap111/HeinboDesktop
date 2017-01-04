using Heinbo.Data;
using HeinboDesktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HeinboDesktop.Orders
{
    public class OrderListViewModel : BindableBase
    {
        private IOrderService _repo;
  
        private List<Order> _allOrders;

        public OrderListViewModel(IOrderService repo)
        {
            _repo = repo;
            OrderProfileCommand = new RelayCommand<Order>(OrderProfile);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set { SetProperty(ref _orders, value); }
        }
        private string _SearchInput;
        public string SearchInput
        {
            get { return _SearchInput; }
            set
            {
                SetProperty(ref _SearchInput, value);
                FilterOrders(_SearchInput);
            }
        }

        private void FilterOrders(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                Orders = new ObservableCollection<Order>(_allOrders);
                return;
            }
            else
            {
                Orders = new ObservableCollection<Order>(_allOrders.Where(c => c.OrderDate.ToString().Contains(searchInput.ToLower())));
            }
        }

        public RelayCommand<Order> OrderProfileCommand { get; private set; }
        public RelayCommand ClearSearchCommand { get; private set; }


     
        public event Action<Order> OrderProfileRequested = delegate { };


        public async void GetOrderList()
        {
            _allOrders = await _repo.GetOrdersAsync();
            Orders = new ObservableCollection<Order>(_allOrders);
        }

        private void OrderProfile(Order order)
        {
            OrderProfileRequested(order);
        }

        private void OnClearSearch()
        {
            SearchInput = null;
        }

    }
}

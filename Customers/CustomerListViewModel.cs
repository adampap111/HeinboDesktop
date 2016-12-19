using Heinbo.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using HeinboDesktop.Services;

namespace HeinboDesktop.Customers
{
    class CustomerListViewModel : BindableBase
    {
        private ICustomersRepository _repo;
        private List<Product> _allCustomers;
        private List<Order> _listOfOrders;
        private List<AspNetUsers> _listOfCustomers;
        

        public CustomerListViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            PlaceOrderCommand = new RelayCommand<Product>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Product>(OnEditCustomer);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        private ObservableCollection<Product> _customers;
        public ObservableCollection<Product> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }
        private string _SearchInput;
        public string SearchInput
        {
            get { return _SearchInput; }
            set
            {
                SetProperty(ref _SearchInput, value);
                FilterCustomers(_SearchInput);
            }
        }

        private void FilterCustomers(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                Customers = new ObservableCollection<Product>(_allCustomers);
                return;
            }
            else
            {
                Customers = new ObservableCollection<Product>(_allCustomers.Where(c => c.ProductName.ToLower().Contains(searchInput.ToLower())));
            }
        }


        public RelayCommand<Product> PlaceOrderCommand { get; private set; }
        public RelayCommand AddCustomerCommand { get; private set; }
        public RelayCommand<Product> EditCustomerCommand { get; private set; }
        public RelayCommand ClearSearchCommand { get; private set; }

        public event Action<int> PlaceOrderRequested = delegate { };
        public event Action<Product> AddCustomerRequested = delegate { };
        public event Action<Product> EditCustomerRequested = delegate { };


        public async void LoadCustomers()
        {
            _allCustomers = await _repo.GetCustomersAsync();
            Customers = new ObservableCollection<Product>(_allCustomers);
          
            _listOfCustomers = await _repo.GetCustomerssAsync();
            _listOfOrders = await _repo.GetOrdersAsync();
        }

        private void OnPlaceOrder(Product customer)
        {
            PlaceOrderRequested(customer.ProductId);
        }

        private void OnAddCustomer()
        {
            AddCustomerRequested(new Product { ProductId = 33 });

        }
        private void OnEditCustomer(Product cust)
        {
            EditCustomerRequested(cust);    
        }

        private void OnClearSearch()
        {
            SearchInput = null;
        }
    }
}

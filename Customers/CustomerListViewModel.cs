using Heinbo.Data;
using HeinboDesktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeinboDesktop.Customers
{
    public class CustomerListViewModel : BindableBase
    {
        private ICustomerService _repo;
        private List<AspNetUsers> _allCustomers;

        public CustomerListViewModel(ICustomerService repo)
        {
            _repo = repo;
            CustomerProfileCommand = new RelayCommand<AspNetUsers>(CustomerProfile);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        private ObservableCollection<AspNetUsers> _customers;
        public ObservableCollection<AspNetUsers> Customers
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
                Customers = new ObservableCollection<AspNetUsers>(_allCustomers);
                return;
            }
            else
            {
                Customers = new ObservableCollection<AspNetUsers>(_allCustomers.
                    Where(c => c.Email.ToLower().Contains(searchInput.ToLower())));
            }
        }

        public RelayCommand<AspNetUsers> CustomerProfileCommand { get; private set; }
        public RelayCommand ClearSearchCommand { get; private set; }

        public event Action<AspNetUsers> EditCustomerRequested = delegate { };


        public async void GetAspNetUsersList()
        {
            _allCustomers = await _repo.GetCustomerList();
            Customers = new ObservableCollection<AspNetUsers>(_allCustomers);
        }

        private void CustomerProfile(AspNetUsers cust)
        {
            EditCustomerRequested(cust);
        }

        private void OnClearSearch()
        {
            SearchInput = null;
        }

    }
}

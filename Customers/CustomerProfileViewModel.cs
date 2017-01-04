using Heinbo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using HeinboDesktop.Services;

namespace HeinboDesktop.Customers
{
    class CustomerProfileViewModel : BindableBase
    {
        private ICustomerService _repo;
        public CustomerProfileViewModel(ICustomerService repo)
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

        private CustomerProfile _Customer;
        public CustomerProfile Customer
        {
            get { return _Customer; }
            set { SetProperty(ref _Customer, value); }
        }

        private AspNetUsers _editingCustomer = null;

        public void SetCustomer(AspNetUsers cust)
        {
            _editingCustomer = cust;
                     Customer = new CustomerProfile();
                       CopyCustomer(cust, Customer);
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


        private void CopyCustomer(AspNetUsers source, CustomerProfile target)
        {
            target.Id = source.Id;
            if (EditMode)
            {
                target.Name = source.LastName + " " + source.FirstName;                
               
                target.Email = source.Email;
                target.PhoneNumber = source.PhoneNumber;
                target.City = source.City;
                target.Zip = source.Zip;
                target.Street = source.Street;
                target.StreetNumber = source.StreetNumber;
               
            }
        }
    }
}

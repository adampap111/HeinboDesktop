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
    class AddEditCustomerViewModel : BindableBase
    {
        private ICustomersRepository _repo;
        public AddEditCustomerViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave); 
        }
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private SimpleEditableCustomer _Customer;
        public SimpleEditableCustomer Customer
        {
            get { return _Customer; }
            set { SetProperty(ref _Customer, value); }
        }

        private Product _editingCustomer = null;

        public void SetCustomer(Product cust)
        {
            _editingCustomer = cust;
            if (Customer != null) Customer.ErrorsChanged -= RaiseCanExecuteChanged;
            Customer = new SimpleEditableCustomer();
            Customer.ErrorsChanged += RaiseCanExecuteChanged; 
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

        private async void OnSave()
        {
            UpdateCustomer(Customer, _editingCustomer);
            if (EditMode)
                await _repo.UpdateCustomerAsync(_editingCustomer);
            else
                await _repo.AddCustomerAsync(_editingCustomer);
            Done();
        }

        private void UpdateCustomer(SimpleEditableCustomer source, Product target)
        {
            target.ProductName = source.ProductName;
            target.Category = source.Category;
            target.Brand = source.Brand;
            target.SubCategory = source.Subcategory;
            target.Size = source.Size;
           
        }

        private bool CanSave()
        {
            return !Customer.HasErrors;
        }

        private void CopyCustomer(Product source, SimpleEditableCustomer target)
        {
            target.Id = source.ProductId;
            if (EditMode)
            {
                target.ProductName = source.ProductName;                
                target.Category = source.Category;
                target.Brand = source.Brand;
                target.Subcategory = source.SubCategory;
                target.Size = source.Size;
               
            }
        }
    }
}

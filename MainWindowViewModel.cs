using HeinboDesktop.Products;
using Microsoft.Practices.Unity;
using Heinbo.Data;
using HeinboDesktop.Customers;
using HeinboDesktop.Orders;

namespace HeinboDesktop
{
    class MainWindowViewModel : BindableBase
    {
        //product
        private ProductListViewModel _productListViewModel;
        private AddEditProductViewModel _addEditViewModel;
        //customer
        private CustomerListViewModel _customerListViewModel;
        private CustomerProfileViewModel _customerProfileViewModel;
        //Order
        private OrderListViewModel _orderListViewModel;
        private OrderProfileViewModel _orderProfileViewModel;
        //common
        private BindableBase _CurrentViewModel;

        public RelayCommand<string> NavCommand { get; private set; }

        public MainWindowViewModel()
        {
            //product
            _productListViewModel = ContainerHelper.Container.Resolve<ProductListViewModel>();
            _addEditViewModel = ContainerHelper.Container.Resolve<AddEditProductViewModel>();
            _productListViewModel.AddCustomerRequested += NavToAddCustomer;
            _productListViewModel.EditProductRequested += NavToEditProduct;
            _addEditViewModel.Done += NavToProductList;
            //customer
            _customerListViewModel = ContainerHelper.Container.Resolve<CustomerListViewModel>();
            _customerListViewModel.EditCustomerRequested += NavToEditCustomer;
            _customerProfileViewModel = ContainerHelper.Container.Resolve<CustomerProfileViewModel>();
            _customerProfileViewModel.Done += NavToCustomerList;
            //order
            _orderListViewModel = ContainerHelper.Container.Resolve<OrderListViewModel>();
            _orderListViewModel.OrderProfileRequested += NavToEditOrder;
            _orderProfileViewModel = ContainerHelper.Container.Resolve<OrderProfileViewModel>();
            _orderProfileViewModel.Done += NavToOrderList;

            NavCommand = new RelayCommand<string>(OnNav);
        }

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }
      
        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "customers":
                    CurrentViewModel = _customerListViewModel;
                    break;
                case "orders":
                    CurrentViewModel = _orderListViewModel;
                    break;
                case "products":
                default:
                    CurrentViewModel = _productListViewModel;
                    break;
            }
        }

        private void NavToAddCustomer(Product cust)
        {
            _addEditViewModel.EditMode = false;
            _addEditViewModel.SetCustomer(cust);
            CurrentViewModel = _addEditViewModel;
        }

        private void NavToEditProduct(Product producct)
        {
            _addEditViewModel.EditMode = true;
            _addEditViewModel.SetCustomer(producct);
            CurrentViewModel = _addEditViewModel;
        }

        private void NavToEditCustomer(AspNetUsers customer)
        {
            _customerProfileViewModel.EditMode = true;
            _customerProfileViewModel.SetCustomer(customer);
            CurrentViewModel = _customerProfileViewModel;
        }

        private void NavToEditOrder(Order order)
        {
            _orderProfileViewModel.EditMode = true;
            _orderProfileViewModel.SerOrder(order);
            CurrentViewModel = _orderProfileViewModel;
        }


        private void NavToProductList()
        {
            CurrentViewModel = _productListViewModel;
        }
        private void NavToCustomerList()
        {
            CurrentViewModel = _customerListViewModel;
        }
        private void NavToOrderList()
        {
            CurrentViewModel = _orderListViewModel;
        }
    }
}

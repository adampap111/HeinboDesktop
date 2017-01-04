﻿using Heinbo.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HeinboDesktop.Services;

namespace HeinboDesktop.Products
{
    class ProductListViewModel : BindableBase
    {
        private IProductService _repo;
        private List<Product> _allProducts;
    


        public ProductListViewModel(IProductService repo)
        {
            _repo = repo;
          
            AddProductCommand = new RelayCommand(OnAddProduct);
            EditProductCommand = new RelayCommand<Product>(OnEditProduct);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Customers
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
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
                Customers = new ObservableCollection<Product>(_allProducts);
                return;
            }
            else
            {
                Customers = new ObservableCollection<Product>(_allProducts.Where(c => c.ProductName.ToLower().Contains(searchInput.ToLower())));
            }
        }


        public RelayCommand AddProductCommand { get; private set; }
        public RelayCommand<Product> EditProductCommand { get; private set; }
        public RelayCommand ClearSearchCommand { get; private set; }

        public event Action<int> PlaceOrderRequested = delegate { };
        public event Action<Product> AddCustomerRequested = delegate { };
        public event Action<Product> EditProductRequested = delegate { };


        public async void GetProductList()
        {
            _allProducts = await _repo.GetProducts();
            Customers = new ObservableCollection<Product>(_allProducts);

        }

        private void OnPlaceOrder(Product customer)
        {
            PlaceOrderRequested(customer.ProductId);
        }

        private void OnAddProduct()
        {
            AddCustomerRequested(new Product { ProductId = 33 });

        }
        private void OnEditProduct(Product cust)
        {
            EditProductRequested(cust);
        }

        private void OnClearSearch()
        {
            SearchInput = null;
        }
    }
}

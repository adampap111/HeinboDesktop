using Heinbo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using HeinboDesktop.Services;

namespace HeinboDesktop.Products
{
    class AddEditProductViewModel : BindableBase
    {
        private IProductService _repo;
        public AddEditProductViewModel(IProductService repo)
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

        private SimpleEditableProduct _Product;
        public SimpleEditableProduct Product
        {
            get { return _Product; }
            set { SetProperty(ref _Product, value); }
        }

        private Product _editingProduct = null;

        public void SetProduct(Product product)
        {
            _editingProduct = product;
        
            Product = new SimpleEditableProduct();
         
            CopyProduct(product, Product);
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
            UpdateProduct(Product, _editingProduct);
            if (EditMode)
                await _repo.UpdateProduct(_editingProduct);
            else
                await _repo.AddProduct(_editingProduct);
            Done();
        }

        private void UpdateProduct(SimpleEditableProduct source, Product target)
        {
            target.ProductName = source.ProductName;
            target.Category = source.Category;
            target.Brand = source.Brand;
            target.SubCategory = source.Subcategory;
            target.Size = source.Size;
           
        }


        private bool CanSave()
        {
            return true;
        }

        private void CopyProduct(Product source, SimpleEditableProduct target)
        {
            target.Id = source.ProductId;
            if (EditMode)
            {
                target.ProductName = source.ProductName;                
                target.Category = source.Category;
                target.Brand = source.Brand;
                target.Subcategory = source.SubCategory;
                target.Size = source.Size;
                target.RetailPrice = source.Price;
               
            }
        }
    }
}

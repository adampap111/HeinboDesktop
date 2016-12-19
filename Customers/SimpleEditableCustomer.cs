using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeinboDesktop.Customers
{
    public class SimpleEditableCustomer : ValidatableBindableBase
    {
        private int  _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _productName;
        [Required]
        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        private string _category;
        [Required]
        public string Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private string _subcategory;
       
        public string Subcategory
        {
            get { return _subcategory; }
            set { SetProperty(ref _subcategory, value); }
        }

        private string _brand;
       
        public string Brand
        {
            get { return _brand; }
            set { SetProperty(ref _brand, value); }
        }

        private int _supplierPrice;
       
        public int SupplierPrice
        {
            get { return _supplierPrice; }
            set { SetProperty(ref _supplierPrice, value); }
        }

        private int _wholeSalePrice;
      
        public int WholeSalePrice
        {
            get { return _wholeSalePrice; }
            set { SetProperty(ref _wholeSalePrice, value); }
        }

        private int _retailPrice;
      
        public int RetailPrice
        {
            get { return _retailPrice; }
            set { SetProperty(ref _retailPrice, value); }
        }

        private string _size;
      
        public string Size
        {
            get { return _size; }
            set { SetProperty(ref _size, value); }
        }


    }
}

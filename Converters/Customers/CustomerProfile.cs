namespace HeinboDesktop.Customers
{
    public class CustomerProfile : ValidatableBindableBase
    {
        public virtual string Id { get; set; }
        public string Name { get; set; }
       
        public int Zip { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string UserName { get; set; }


    }
}

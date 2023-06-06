class Customer : AccountModel
{
    public Customer(int id, string emailAddress, string password, string fullName, int level)
        : base(id, emailAddress, password, fullName, level)
    {
        // Additional customer-specific properties and logic can be added here
    }
}

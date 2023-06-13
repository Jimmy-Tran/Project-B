class Employee : AccountModel, IAccount
{
    public Employee(int id, string emailAddress, string password, string fullName, int level)
        : base(id, emailAddress, password, fullName, level)
    {
        // Additional employee-specific properties and logic can be added here
    }
}

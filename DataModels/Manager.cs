class Manager : AccountModel, IAccount
{
    public Manager(int id, string emailAddress, string password, string fullName, int level)
        : base(id, emailAddress, password, fullName, level)
    {
        // Additional manager-specific properties and logic can be added here
    }
}
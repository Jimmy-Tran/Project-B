using System.Text.Json.Serialization;


class AccountModel: IAccount
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    public AccountModel(int id, string emailAddress, string password, string fullName, int level)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        Level = level;
    }

}





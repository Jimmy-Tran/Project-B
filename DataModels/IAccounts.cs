using System.Text.Json.Serialization;

interface IAccount
{
    [JsonPropertyName("id")]
    int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    string Password { get; set; }

    [JsonPropertyName("fullName")]
    string FullName { get; set; }

    [JsonPropertyName("level")]
    int Level { get; set; }
}
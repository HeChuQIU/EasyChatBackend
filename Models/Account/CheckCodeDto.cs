using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyChatBackend.Models.Account;

public record CheckCodeDto
{
    public CheckCodeDto(string CheckCodeBase64,
        string CheckCodeKey)
    {
        this.CheckCodeBase64 = $"data:image/png;base64,{CheckCodeBase64}";
        this.CheckCodeKey = CheckCodeKey;
    }

    [Required, JsonPropertyName("checkCode")]
    public string CheckCodeBase64
    {
        get;
        init;
    }

    [Required, JsonPropertyName("checkCodeKey")]
    public string CheckCodeKey
    {
        get;
        init;
    }

    public void Deconstruct(out string CheckCodeBase64, out string CheckCodeKey)
    {
        CheckCodeBase64 = this.CheckCodeBase64;
        CheckCodeKey = this.CheckCodeKey;
    }
}
using Newtonsoft.Json;
using VkNet.Enums.SafetyEnums;
using VkNet.Utils;
using VkNet.Utils.JsonConverter;

#pragma warning disable CS8618

namespace Soofle.Infrastructure.VkRequests.Models;

public class Subscription : IVkModel
{
    /// <summary>Преобразовать из JSON</summary>
    /// <param name="response"> Ответ от сервера. </param>
    /// <returns> </returns>
    IVkModel IVkModel.FromJson(VkResponse response) => FromJson(response);

    /// <summary>Разобрать из json.</summary>
    /// <param name="response"> Ответ сервера. </param>
    /// <returns> </returns>
    public static Subscription FromJson(VkResponse response)
    {
        return new()
        {
            Id = (long)(response["group_id"] ?? response["gid"] ?? response["id"]),
            Name = response["name"],
            Type = response["type"],
            Deactivated = response["deactivated"],
            FirstName = response["first_name"],
            LastName = response["last_name"]
        };
    }

    /// <summary>Идентификатор сообщества.</summary>
    public long Id { get; set; }

    /// <summary>Название сообщества.</summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Возвращается в случае, если сообщество удалено или заблокировано
    /// </summary>
    [JsonProperty("deactivated"), JsonConverter(typeof(SafetyEnumJsonConverter))]
    public Deactivated Deactivated { get; set; }

    [JsonProperty("first_name")] public string FirstName { get; set; }

    [JsonProperty("last_name")] public string LastName { get; set; }


    /// <summary>Тип сообщества.</summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(SafetyEnumJsonConverter))]
    public GroupType Type { get; set; }
}
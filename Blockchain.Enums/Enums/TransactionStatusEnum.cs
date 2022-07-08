using System;
using Newtonsoft.Json;

namespace Blockchain.Enums
{
    [JsonConverter(typeof(DefaultUnknownEnumConverter))]
    public enum TransactionStatusEnum
	{
        Unknown = 0,
        Panding = 1,
        Success = 2,
        Expire = 3,
        Decline = 4,
    }
}


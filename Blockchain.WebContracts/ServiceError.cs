using System;
namespace Blockchain.WebContracts
{
	public class ServiceError
	{
        public ServiceError(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; private set; }
        public string Description { get; private set; }
    }
}


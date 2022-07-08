using System;
using System.Collections.Generic;
using System.Net;

namespace Blockchain.WebContracts
{
	public class ServiceResult
	{
        public bool IsOk { get; protected set; }
        public bool IsError => !IsOk;
        public HttpStatusCode Status { get; protected set; }
        public List<ServiceError> ErrorMessages { get; set; } = new List<ServiceError>();

        public ServiceResult(HttpStatusCode status, bool isOk)
        {
            Status = status;
            IsOk = isOk;
        }

        public virtual void AddError(string code, string description)
        {
            ServiceError error = new ServiceError(code, description);
            ErrorMessages.Add(error);
        }
    }
}


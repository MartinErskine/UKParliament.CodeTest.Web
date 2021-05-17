using System.Net;

namespace UKParliament.CodeTest.Services.Helpers
{
    /// <summary>
    /// Wrapper around the response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
        }

        public ServiceResponse(HttpStatusCode? errorCode)
        {
            this.ErrorCode = errorCode;
        }

        public ServiceResponse(HttpStatusCode? errorCode, string errorDescription)
        {
            this.ErrorCode = errorCode;
            this.ErrorDescription = errorDescription;
        }

        public ServiceResponse(T data)
        {
            this.Data = data;
        }

        public HttpStatusCode? ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        public bool IsError
        {
            get
            {
                return ErrorCode.HasValue || !string.IsNullOrEmpty(ErrorDescription);
            }
        }

        public T Data { get; set; }



        public static ServiceResponse<T> Error(string message)
        {
            return new ServiceResponse<T>()
            {
                ErrorCode = HttpStatusCode.NotAcceptable,
                ErrorDescription = message
            };
        }
        public static ServiceResponse<T> NotFound(string subject)
        {
            return new ServiceResponse<T>()
            {
                ErrorCode = HttpStatusCode.NotFound,
                ErrorDescription = $"{subject} does not exist."
            };
        }

        public static ServiceResponse<T> Success(T responseModel)
        {
            return new ServiceResponse<T>(responseModel);
        }
        public static ServiceResponse<T> Empty()
        {
            return new ServiceResponse<T>(default(T));
        }
    }

    public class ServiceResponseModel
    {
        public ServiceResponseModel()
        {
        }

        public ServiceResponseModel(HttpStatusCode? errorCode)
        {
            this.ErrorCode = errorCode;
        }

        public ServiceResponseModel(HttpStatusCode? errorCode, string errorDescription)
        {
            this.ErrorCode = errorCode;
            this.ErrorDescription = errorDescription;
        }

        public HttpStatusCode? ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        public bool IsError
        {
            get
            {
                return ErrorCode.HasValue || !string.IsNullOrEmpty(ErrorDescription);
            }
        }
    }
}


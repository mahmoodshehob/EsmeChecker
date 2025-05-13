using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.Models.Helper
//{
//	public class ServiceResponse<T>
//	{
//		public ServiceResponse(bool isSuccessResultStatus = false)
//		{
//			this.IsSuccessResultStatus = isSuccessResultStatus;
//			Message = isSuccessResultStatus ? "Operation Success" : "Operation Failed";
//		}

//		public T? Response { get; set; } = default;
//		public bool IsSuccessResultStatus { get; set; }
//		public string? StatusCode { get; set; }
//		public string? Message { get; set; }
//		public string? Details { get; set; }
//		public string? Description { get; set; }
//	}
//}

{

	public class ServiceResponse<T>
	{
		public ServiceResponse()
		{
			IsSuccess = false;
			StatusCode = HttpStatusCode.BadRequest;
			Message = "Operation failed";
		}

		public ServiceResponse(T response,
							   HttpStatusCode statusCode = HttpStatusCode.OK,
							   string message = "Operation succeeded")
		{
			Response = response;
			StatusCode = statusCode;
			IsSuccess = statusCode == HttpStatusCode.OK;
			Message = message;
		}

		public T? Response { get; set; }
		public bool IsSuccess { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public string Message { get; set; }
		public string? Details { get; set; }
		public string? Description { get; set; }

		public static ServiceResponse<T> Success(T response, string message = "Operation succeeded")
		{
			return new ServiceResponse<T>(response, HttpStatusCode.OK, message);
		}

		public static ServiceResponse<T> Failure(string message = "Operation failed",
												 HttpStatusCode statusCode = HttpStatusCode.BadRequest,
												 string? details = null)
		{
			return new ServiceResponse<T>
			{
				Response = default,
				IsSuccess = false,
				StatusCode = statusCode,
				Message = message,
				Details = details
			};
		}
	}
}

namespace solution.Models
{
	public class ApiResult<T>
	{
		public bool Successful { get; set; }
		public string SuccessMessage { get; set; }
		public bool Failed { get; set; }
		public object FailedReason { get; set; }

		public T Data { get; set; }

		public static ApiResult<T> SuccessfulResult(T model, string successMessage = "")
		{
			return new ApiResult<T>
			{
				Successful = true,
				Data = model,
				SuccessMessage = successMessage
			};
		}

		public static ApiResult<T> UnsuccessfulResult(string reason)
		{
			return new ApiResult<T>
			{
				Successful = false,
				Failed = true,
				FailedReason = reason
			};
		}
	}
}

using Newtonsoft.Json;

namespace Homework.RequestDataModel
{
	public class GetUserNameRequest
	{
		[JsonProperty]
		public Guid userGuid { get; set; }
	}
}

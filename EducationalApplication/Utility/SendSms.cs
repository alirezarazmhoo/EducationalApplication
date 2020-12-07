using SmsIrRestful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Utility
{
	public static class SendSms
	{

		static Token tk = new Token();
		public static string userApiKey { get; } = "bd117f7d7554053b8e6d72fc";
		public static string secretKey { get; } = "AtrinEducationApp";
		public static string LineNumber { get; } = "30004005550505";

		static List<UltraFastParameters> UltraFastParameters = new List<UltraFastParameters>();
		public static bool CallSmSMethod(long MobileNumber , int TemplateId , string ParameterName , string ParameterValue)
		{
			var ultraFastSend = new UltraFastSend()
			{
				Mobile = MobileNumber,
				TemplateId = TemplateId,
				ParameterArray = new List<UltraFastParameters>()
				{

				 new UltraFastParameters()
				  {
				   Parameter = ParameterName , ParameterValue = ParameterValue
				  }
				 }.ToArray()
			};
			UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(tk.GetToken(userApiKey, secretKey), ultraFastSend);
			return ultraFastSendRespone.IsSuccessful;
		}
		public static bool CallSmSMethodAdvanced(long MobileNumber, int TemplateId,List<SmsParameters> Parameter)
		{
			foreach (var item in Parameter)
			{
			UltraFastParameters.Add(new UltraFastParameters() { Parameter = item.Parameter, ParameterValue = item.ParameterValue});
			}
			var ultraFastSend = new UltraFastSend()
			{
				Mobile = MobileNumber,
				TemplateId = TemplateId,
				ParameterArray = UltraFastParameters.ToArray()
			};
				UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(tk.GetToken(userApiKey, secretKey), ultraFastSend);
			return ultraFastSendRespone.IsSuccessful;
		}
	}
	public class SmsParameters
	{
		public string Parameter { get; set; }
		public string ParameterValue { get; set; }
	}

}
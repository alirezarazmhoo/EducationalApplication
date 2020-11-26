using EducationalApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Utility
{
	public static class ApiResponse
	{  
		public static ApiModel Success(object  data=null)
		{
			ApiModel apiModel = new ApiModel();
			apiModel.status = 200;
			apiModel.success = true;
			apiModel.message = "عملیات انجام شد";
			apiModel.data = data;
			return apiModel; 

		}
		public static ApiModel Fail(object data , int status=500 , string message= "عملیات با شکست مواجه شد.")
		{
			ApiModel apiModel = new ApiModel();
			apiModel.status = status;
			apiModel.success = false;
			apiModel.message = message;
			apiModel.data = data;
			return apiModel;
		}

	}
}

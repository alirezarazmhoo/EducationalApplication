using EducationalApplication.Models;
using EducationalApplication.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Utility
{
	public static class FormatCheck
	{

		public static MediaType GetFormat(IFormFile File)
		{

			if ((string.Equals(File.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "image/png", StringComparison.OrdinalIgnoreCase)))
			{
				return MediaType.Picture;
			}
			else if ((string.Equals(File.ContentType, "audio/WAV", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "audio/MID", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "audio/MIDI", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "audio/WMA", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "audio/MP3", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "audio/OGG", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "audio/RMA", StringComparison.OrdinalIgnoreCase) ||
			  string.Equals(File.ContentType, "audio/mpeg", StringComparison.OrdinalIgnoreCase)))
			{
				return MediaType.Audio;
			}
			else if ((string.Equals(File.ContentType, "video/AVI", StringComparison.OrdinalIgnoreCase) ||
			string.Equals(File.ContentType, "video/MP4", StringComparison.OrdinalIgnoreCase) ||
			string.Equals(File.ContentType, "video/DIVX", StringComparison.OrdinalIgnoreCase) ||
			string.Equals(File.ContentType, "video/WMV", StringComparison.OrdinalIgnoreCase)
			|| string.Equals(File.ContentType, "video/DIVX", StringComparison.OrdinalIgnoreCase)
			|| string.Equals(File.ContentType, "video/mkv", StringComparison.OrdinalIgnoreCase)))
			{
				return MediaType.Video;
			}
			else if (string.Equals(File.ContentType,
				"application/pdf", StringComparison.OrdinalIgnoreCase))
			{
				return MediaType.Pdf;
			}
			else if (string.Equals(File.ContentType,
				"application/vnd.openxmlformats-officedocument.wordprocessingml.document",
				StringComparison.OrdinalIgnoreCase))
			{
				return MediaType.Word;
			}
			else if (string.Equals(File.ContentType,
				"application/vnd.openxmlformats-officedocument.presentationml.presentation",
				StringComparison.OrdinalIgnoreCase))
			{
				return MediaType.PowerPoint;
			}
			else
			{
				return MediaType.Undefined;
			}

		}
		public static MediaType GetFormat(string url)
		{
			string[] _UrlList = new string[] { };
			_UrlList = url.Split(".");
			var lstitem = _UrlList[_UrlList.Length - 1];
			if (string.Equals(lstitem, "AVI", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "MP4", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "DIVX", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "WMV", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "mkv", StringComparison.OrdinalIgnoreCase))
			{
				return MediaType.Video;
			}
			else if (string.Equals(lstitem, "jpg", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "jpeg", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "DIVX", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "pjpeg", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "gif", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "png", StringComparison.OrdinalIgnoreCase) ||
				  string.Equals(lstitem, "x-png", StringComparison.OrdinalIgnoreCase))
			{
				return MediaType.Picture;
			}

			else if (string.Equals(lstitem, "WAV", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "MID", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "MIDI", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "WMA", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "MP3", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "OGG", StringComparison.OrdinalIgnoreCase) ||
				  string.Equals(lstitem, "RMA", StringComparison.OrdinalIgnoreCase) || string.Equals(lstitem, "mpeg", StringComparison.OrdinalIgnoreCase))
			{
				return MediaType.Audio;
			}
			else if(string.Equals(lstitem, "pdf", StringComparison.OrdinalIgnoreCase))
			{
				return MediaType.Pdf; 
			}

			else if (string.Equals(lstitem, "docx", StringComparison.OrdinalIgnoreCase))
			{
				return MediaType.Word;
			}
			else
			{
				return MediaType.Undefined;
			}

		
		}

	}
}

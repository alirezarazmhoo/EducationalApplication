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
            else if((string.Equals(File.ContentType, "audio/WAV", StringComparison.OrdinalIgnoreCase) ||
              string.Equals(File.ContentType, "audio/MID", StringComparison.OrdinalIgnoreCase) ||
              string.Equals(File.ContentType, "audio/MIDI", StringComparison.OrdinalIgnoreCase) ||
              string.Equals(File.ContentType, "audio/WMA", StringComparison.OrdinalIgnoreCase) ||
              string.Equals(File.ContentType, "audio/MP3", StringComparison.OrdinalIgnoreCase) ||
              string.Equals(File.ContentType, "audio/OGG", StringComparison.OrdinalIgnoreCase)||
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
            else if(string.Equals(File.ContentType, 
                "application/pdf", StringComparison.OrdinalIgnoreCase)){
                return MediaType.Pdf; 
            }
            else if (string.Equals(File.ContentType,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                StringComparison.OrdinalIgnoreCase)){
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


	}
}

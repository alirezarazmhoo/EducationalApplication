using Microsoft.Extensions.FileProviders;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using NAudio.Wave;
using NReco.VideoInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Utility
{
	public static class DurationFile
	{
        public static string GetDuration(string filePath)
        {

            string p = filePath;
            string fileExt = Path.GetExtension(p);
            string time;
            if (fileExt == ".mp3")
            {
                //return new Mp3FileReader(filePath).TotalTime;
                time = new Mp3FileReader(filePath).TotalTime.ToString();
                return time; 
            }
            else
            {
                time = "0";
                return time;
            }  
        }
    }
}

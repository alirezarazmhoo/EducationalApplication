using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Models;
using Microsoft.AspNetCore.Http;

namespace EducationalApplication.Infrastructure
{
   public interface IBanerServices
   {
       Task<List<Banner>> GetAllBaner();
       Task<Banner> GetbanerById(int banerId);
       Task AddBaner(Banner baner, IFormFile BanerAvatar);
       Task Editbanner( Banner baner, IFormFile BanerAvatar);
       void Remove(Banner baner);
   }
}

using EducationalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
  public  interface IFavoritRepo
    {
        Task Create(Favorit model );
      void   Remove(Favorit model);
    }
}

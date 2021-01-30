using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
    public class Favorit
    {
        public int Id { get; set; }
        public int EducationPostId { get; set; }
        public EducationPost EducationPost { get; set; }
        public int BannerId { get; set; }
        public Banner Banner { get; set; }
        public int StudentsId { get; set; }
        public Students Students { get; set; }
    }
}

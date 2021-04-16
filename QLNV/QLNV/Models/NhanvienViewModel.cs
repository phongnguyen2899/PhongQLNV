using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLNV.Models
{
    public class NhanvienViewModel
    {
        public int Id { get; set; }

        public string CV { get; set; }
        public string Ten { get; set; }

        public DateTime Ngaysinhh { get; set; }
        public string Diachi { get; set; }

        public string SDT { get; set; }

        public string Email { get; set; }
        public string Tenchucvu { get; set; }
        public string Tenvitri { get; set; }
        public string Nguoigioithieu { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DATA.Entity
{
    public class Chucvu
    {
        public int Id { get; set; }
        public string Tenchucvu { get; set; }

        public ICollection<Nhanvien> Nhanviens { get; set; }
    }
}

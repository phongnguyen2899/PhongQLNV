using System;
using System.Collections.Generic;
using System.Text;

namespace DATA.Entity
{
    public class Vitri
    {
        public int Id { get; set; }
        public string Tenvitri { get; set; } 
        public ICollection<Nhanvien> Nhanviens { get; set; }
    }
}

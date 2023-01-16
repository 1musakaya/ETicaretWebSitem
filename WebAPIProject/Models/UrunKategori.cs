using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIProject.Models
{
    public class UrunKategori
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public int KategoriID { get; set; }
        public string UrunAciklamasi { get; set; }
        public int UrunFiyati { get; set; }
        public string KategoriAdi { get; set; }
    }
}
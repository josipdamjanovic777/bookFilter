using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnjigeFilter
{
    class Knjiga
    {
        public string Autor { get; set; }
        public string Naziv { get; set; }
        public int BrojStrana { get; set; }
        public string TipKnjige { get; set; }
        public Knjiga(string a, string n, int brs,string t)
        {
            Autor = a;
            Naziv = n;
            BrojStrana = brs;
            TipKnjige = t;
        }
        public static List<Knjiga> knjige = new List<Knjiga>
    {
        new Knjiga("Antun-Gustav-Matos","Vlak u Snijegu",300,"Roman"),
        new Knjiga("Tolstoj","Zlocin i Kazna",500,"Krimic"),
        new Knjiga("Ivana Mazuranic","Vlak u Snijegu",300,"Roman"),
        new Knjiga("Mark Twain","Pustolovine H.Finna",400,"Roman"),
        new Knjiga("Mark Twain","Knjiga2",500,"Krimic")
    };
    }
}

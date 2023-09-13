using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnjigeFilter
{
    public partial class Form1 : Form
    {
        //Lista u koju spremamo filtrirane vrijednosti
        BindingList<Knjiga> FiltriraneKnjige = new BindingList<Knjiga>();

        List<string> Filteri = new List<string>();
        public Form1()
        {
            InitializeComponent();
            //inicijalno popunjavamo tablicu sa svim knjigama
            dataGridView1.DataSource = new BindingList<Knjiga>(Knjiga.knjige);

            //trazimio sve razlicite tipove knjiga
            foreach (var knjiga in Knjiga.knjige)
            {
                bool Pronadjen = false;
                foreach (var filter in Filteri)
                {
                    if (knjiga.TipKnjige == filter)
                    {
                        Pronadjen = true;
                        break;
                    }
                }
                if (!Pronadjen)
                {
                    Filteri.Add(knjiga.TipKnjige);
                }
            }

            //Kraci nacin
                //Filteri = Knjiga.knjige.Select(k => k.TipKnjige).Distinct().ToList();
            //Iscrtavanje dinamickih checkBoxova
            //Koordinate prvog checkboxa
            int x = 300;
            int y = 15;
            foreach (var kontrola in Filteri)
            {
                CheckBox c = new CheckBox();
                //ime chechboxa
                c.Text = kontrola;
                //kacimo event koji vrijedi za svaki checkbox
                c.CheckedChanged += checkBox1_CheckedChanged;
                c.Location = new System.Drawing.Point(x, y);
                this.Controls.Add(c);
                x += 110;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //ako ima nesto u filter listi koristimo nju, u suprotnom sve knjige
            BindingList<Knjiga> Pretrazivanje = FiltriraneKnjige.Count != 0 ? FiltriraneKnjige : new BindingList<Knjiga>(Knjiga.knjige);
            TextBox t = sender as TextBox;
            //filtriramo Pretrazivanje tako da uzimamo one knjige ciji naziv, sadrzi tekst searchboxa iz forme
            //sa || uvjetom mozemo dodati visestruke kriterije
            dataGridView1.DataSource = Pretrazivanje.Where(k => k.Naziv.ToLower().Contains(t.Text.ToLower())).ToList();
            dataGridView1.Refresh();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            //ako je checkbox oznacen, onda dodajemo sve knjiga tog tipa u filter listu
            //text od checkboxa je namjesten na formi da ima istu vrijednost kao tip knjiga, u svrhu dinamickog iscrtavanja
            if (c.Checked == true)
            {
                foreach (var item in Knjiga.knjige)
                {
                    if (item.TipKnjige.ToLower() == c.Text.ToLower())
                    {
                        FiltriraneKnjige.Add(item);
                    }
                }
            }
            //ako je odznacen brisemo knjige
            else
            {
                foreach (var item in Knjiga.knjige)
                {
                    if (item.TipKnjige.ToLower() == c.Text.ToLower())
                    {
                        FiltriraneKnjige.Remove(item);
                    }
                }
            }
            //kraci nacin koristenjem linq a ne for petlje
            //FiltriraneKnjige = Knjiga.knjige.Where(k => k.TipKnjige.ToLower() == c.Text.ToLower()).ToList();

            //na kraju pozivamo event od textchanged, koji ce u biti ovu nasu filtriranu listu dalje filtrirati po trazenom nazivu
            //ovako zaobilazimo potencijalne bugove, ako je npr u trazilici vec upisano slovo a, a mi stisenemo sve romane
            //onda bi se filtrirali svi romani, a ne oni koji sadrže slovo a
            textBox1_TextChanged(textBox1, new EventArgs());
        }
    }
}

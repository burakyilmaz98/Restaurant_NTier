using BLL.Repositories;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUIMarla
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MenuKategoriRepository crp = new MenuKategoriRepository();
        UrunRepository Urp = new UrunRepository();
        SiparisRepository spr = new SiparisRepository();
        CalisanRepository calisanrp = new CalisanRepository();
        UrunSiparisDetayRepository Usd = new UrunSiparisDetayRepository();

        private void Form1_Load(object sender, EventArgs e)
        {
           


            comboMenukat.DataSource = crp.GetAll();
            comboMenukat.DisplayMember = "MenuKategoriAdi";
            comboMenukat.ValueMember = "MenuKategoriID";

            comboSiparisCalisan.DataSource = calisanrp.GetAll();
            comboSiparisCalisan.DisplayMember = "CalisanAdi";
            comboSiparisCalisan.ValueMember = "CalisanID";
        }

        private void btnMenukategoriGoster_Click(object sender, EventArgs e)
        {
            GetirMenuKategori();

        }

        private void GetirMenuKategori()
        {
            dataGridViewMenuKategori.DataSource = crp.GetAll().Select(c => new { c.MenuKategoriID, c.MenuKategoriAdi, c.MenuID }).ToList();
        }

        private void btnMenuKategoriEkle_Click(object sender, EventArgs e)
        {
            crp.Insert(new MenuKategori {MenuKategoriAdi=textBox1.Text,MenuID=Convert.ToInt32( textBox2.Text) });
            GetirMenuKategori();
            TemizlemenKat();
        }

        MenuKategori secili;
        private void dataGridViewMenuKategori_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int id = (int)dataGridViewMenuKategori.Rows[e.RowIndex].Cells[0].Value;
            secili = crp.GetById(id);
            textBox1.Text = secili.MenuKategoriAdi;
            textBox2.Text = secili.MenuID.ToString();

        }

        private void btnMenuKategoriGuncelle_Click(object sender, EventArgs e)
        {


            secili.MenuKategoriAdi = textBox1.Text;
            secili.MenuID = Convert.ToInt32(textBox2.Text);
            crp.Update(secili);
            GetirMenuKategori();
            TemizlemenKat();

        }

        private void TemizlemenKat()
        {
            textBox1.Text = textBox2.Text = string.Empty;
        }

        private void btnMenuKategoriSil_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewMenuKategori.SelectedCells[0].Value;
            crp.Delete(id);
            GetirMenuKategori();
            TemizlemenKat();
        }

        private void btnMenugoster_Click(object sender, EventArgs e)
        {
            GetirUrun();
        }

        private void GetirUrun()
        {
            dataGridViewUrun.DataSource = Urp.GetAll().Select(c => new { c.UrunID, c.KategoriID, c.UrunAdi, c.UrunAciklamasi, c.Fiyat, c.Durum, }).ToList();
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            Urp.Insert(new Urun { KategoriID = (int)comboMenukat.SelectedValue, UrunAdi = txtUrunAdi.Text, UrunAciklamasi = txtUrunAciklama.Text, Fiyat = Convert.ToDecimal(txturunFiyat.Text), Durum = checkUrun.Checked });
            GetirUrun();
            TemizleUrun();

        }

        private void TemizleUrun()
        {
            txtUrunAdi.Text = txtUrunAciklama.Text = txturunFiyat.Text = string.Empty;
            checkUrun.Checked = false;
        }

        Urun SeciliUrun;
        private void dataGridViewUrun_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridViewUrun.Rows[e.RowIndex].Cells[0].Value;
            SeciliUrun = Urp.GetById(id);
            comboMenukat.SelectedValue = SeciliUrun.KategoriID;
            txtUrunAdi.Text = SeciliUrun.UrunAdi;
            txtUrunAciklama.Text = SeciliUrun.UrunAciklamasi;
            txturunFiyat.Text = SeciliUrun.Fiyat.ToString();
            checkUrun.Checked = (bool)SeciliUrun.Durum;

        
        }

        private void btnUrunGuncelle_Click(object sender, EventArgs e)
        {
            SeciliUrun.KategoriID = (int)comboMenukat.SelectedValue;
            SeciliUrun.UrunAdi = txtUrunAdi.Text;
            SeciliUrun.UrunAciklamasi = txtUrunAciklama.Text;
            SeciliUrun.Fiyat = Convert.ToDecimal( txturunFiyat.Text);
            SeciliUrun.Durum = checkUrun.Checked;
            Urp.Update(SeciliUrun);
            TemizlemenKat();

            TemizleUrun();
            GetirUrun();
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewUrun.SelectedCells[0].Value;
            Urp.Delete(id);
            GetirUrun();
            TemizleUrun();
        }

        private void btnSiparisGoster_Click(object sender, EventArgs e)
        {
            GetirSiparis();
        }

        private void GetirSiparis()
        {
            dataGridViewSiparis.DataSource = spr.GetAll().Select(c => new { c.SiparisID, c.SiparisTarihi, c.TeslimTarihi, c.SiparisTuru, c.MasaNumarası, c.MusteriID, c.CalisanId }).ToList();
        }

        private void btnSiparisEkle_Click(object sender, EventArgs e)
        {
            if (checkSiparisTuru.Checked == true)
            {
                spr.Insert(new Sipari { SiparisTarihi = DateTime.Now, TeslimTarihi = DateTime.Now.AddMinutes(Convert.ToDouble(txtSiparisSuresi.Text)), SiparisTuru = checkSiparisTuru.Checked, MusteriID = Convert.ToInt32(txtSiparisMusteriID.Text) });

            }
            else if (checkSiparisTuru.Checked == false)
            {
                spr.Insert(new Sipari { SiparisTarihi = DateTime.Now, TeslimTarihi = DateTime.Now.AddMinutes(Convert.ToDouble(txtSiparisSuresi.Text)), SiparisTuru = checkSiparisTuru.Checked, MusteriID = Convert.ToInt32(txtSiparisMusteriID.Text), MasaNumarası = Convert.ToInt32(txtSiparisMasaNumarasi.Text), CalisanId = (int)comboSiparisCalisan.SelectedValue });

                GetirSiparis();

            }
            TemizleSiparis();
        }

        private void TemizleSiparis()
        {
            txtSiparisSuresi.Text = txtSiparisMusteriID.Text = txtSiparisMasaNumarasi.Text = string.Empty;
            checkSiparisTuru.Checked = false;
        }

        Sipari SeciliSiparis;
        private void dataGridViewSiparis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (checkSiparisTuru.Checked==false)
            {
                int id = (int)dataGridViewSiparis.Rows[e.RowIndex].Cells[0].Value;
                SeciliSiparis = spr.GetById(id);
                //txtSiparisSuresi.Text = SeciliSiparis.TeslimTarihi;
           
                checkSiparisTuru.Checked = (bool)SeciliSiparis.SiparisTuru;
                txtSiparisMusteriID.Text = SeciliSiparis.MusteriID.ToString();

            }
            else if (checkSiparisTuru.Checked == true)
            {
                int id = (int)dataGridViewSiparis.Rows[e.RowIndex].Cells[0].Value;
                SeciliSiparis = spr.GetById(id);
                //txtSiparisSuresi.Text = SeciliSiparis.TeslimTarihi;
                txtSiparisMasaNumarasi.Text = SeciliSiparis.MasaNumarası.ToString();
                checkSiparisTuru.Checked = (bool)SeciliSiparis.SiparisTuru;
                txtSiparisMusteriID.Text = SeciliSiparis.MusteriID.ToString();
                comboSiparisCalisan.SelectedValue = SeciliSiparis.CalisanId;
            }
        }

        private void btnSiparisGuncelle_Click(object sender, EventArgs e)
        {
            SeciliSiparis.MasaNumarası = Convert.ToInt32( txtSiparisMasaNumarasi.Text);
            SeciliSiparis.SiparisTuru = checkSiparisTuru.Checked;
            SeciliSiparis.MusteriID = Convert.ToInt32( txtSiparisMusteriID.Text);
            SeciliSiparis.CalisanId = (int)comboSiparisCalisan.SelectedValue;
            GetirSiparis();
            TemizleSiparis();
    
        }

        private void btnSiparisSil_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewSiparis.SelectedCells[0].Value;
            spr.Delete(id);
            GetirSiparis();
            TemizleSiparis();
        }

        private void btnUrnSiparisGoster_Click(object sender, EventArgs e)
        {
           dataGridViewUrunSiparis.DataSource= Usd.GetAll().Select(c => new { c.UrunSiparisDetay1, c.UrunID,  c.SiparisID, c.SiparisMiktari, c.Fiyat }).ToList(); ;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;

namespace kargo16220049
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=kargo.mdb");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            verileriCek();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {

                if (kontrol(knoSorgula.Text) == true)
                {
                    baglanti.Open();
                    String sql = "SELECT * FROM kargo WHERE Takip_Kodu LIKE '" + knoSorgula.Text + "%'";
                    DataSet kume = new DataSet();
                    OleDbDataAdapter da = new OleDbDataAdapter(sql, baglanti);

                    da.Fill(kume, "kargo");
                    sorgulaGrid.ItemsSource = kume.Tables["kargo"].DefaultView;

                    baglanti.Close();
                }
                else
                    MessageBox.Show("Buraya sayı değeri girilmeli");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
         



        public void verileriCek()
        {
            try
            {
                baglanti.Open();

                DataSet kume = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM kargo", baglanti);
                da.Fill(kume, "kargo");

                guncelleGrid.ItemsSource = kume.Tables["kargo"].DefaultView;
                izgara2.ItemsSource = kume.Tables["kargo"].DefaultView;
                
                baglanti.Close();
                
            }
            catch (Exception exc)
            {
                if(baglanti.State == ConnectionState.Open)
                    baglanti.Close();
                 MessageBox.Show(exc.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                bool a = false;
                if (kontrol(Gtel.Text) == kontrol(Atel.Text) == kontroltel(Atel.Text) == kontroltel(Gtel.Text)  == true)
                    a = true;
                if (a == true)
                {

                    baglanti.Open();
                    string sql = "INSERT INTO kargo ( Takip_Kodu, Gönderen, Gönderen_adresi, Gönderen_telefon, Alıcı, Alıcı_adres, Alıcı_telefon ) VALUES (@Kno, @Gad, @Gadres, @Gtel, @Asoyad, @Aadres, @Atel)";


                    OleDbCommand komut = new OleDbCommand(sql, baglanti);


                    komut.Parameters.AddWithValue("@Kno", kNo.Text);
                    komut.Parameters.AddWithValue("@Gad", Gas.Text);
                    komut.Parameters.AddWithValue("@Gadres", Gadres.Text);
                    komut.Parameters.AddWithValue("@Gtel", Gtel.Text);
                    komut.Parameters.AddWithValue("@Asoyad", AAS.Text);
                    komut.Parameters.AddWithValue("@Aadres", AAdres.Text);
                    komut.Parameters.AddWithValue("@Atel", Atel.Text);


                    komut.ExecuteNonQuery();

                    baglanti.Close();
                    MessageBox.Show("Kargo Eklendi. ");
                    verileriCek();
                }
                else
                {
                    MessageBox.Show("Eklemek istediginiz Telefon numarası veya verilerde geçersiz birşey var Telefon numaraları 11 sayı'dan oluşmalıdır."); 
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                baglanti.Close();
            }
        
        }

        bool kontrol(string text)
        {
            foreach (char chr in text)
            {
                if (!Char.IsNumber(chr)) return false;
            }
            return true;
        }

        bool kontroltel(string text)
        {
            int sayac = 0;
            foreach (char chr in text)
            {
                sayac++;
            }
            if (sayac == 11) 
            return true;
            else
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                baglanti.Open();
                String sql = "SELECT * FROM kargo WHERE Takip_Kodu LIKE '" + sorgulamaaa.Text + "%'";
                DataSet kume = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter(sql, baglanti);

                da.Fill(kume, "kargo");
                guncelleGrid.ItemsSource = kume.Tables["kargo"].DefaultView;
                izgara2.ItemsSource = kume.Tables["kargo"].DefaultView;

                baglanti.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                baglanti.Open();
                String sql = "UPDATE kargo SET Takip_Kodu=@No, Gönderen=@gönderen, Gönderen_adresi=@gadres, Gönderen_telefon=@gtelefon, Alıcı=@alıcı, Alıcı_adres=@aadres, Alıcı_telefon=@atelefon WHERE Takip_Kodu=@No";
                OleDbCommand komut = new OleDbCommand(sql, baglanti);

                komut.Parameters.AddWithValue("@No", guncelle1.Text);
                komut.Parameters.AddWithValue("@gönderen", guncelle2.Text);
                komut.Parameters.AddWithValue("@gadres", Guncelle3.Text);
                komut.Parameters.AddWithValue("@gtelefon", guncelle4.Text);
                komut.Parameters.AddWithValue("@alıcı",  guncelle5.Text);
                komut.Parameters.AddWithValue("@aadres", guncelle6.Text);
                komut.Parameters.AddWithValue("@atelefon", guncelle7.Text);
               

                komut.ExecuteNonQuery();

                baglanti.Close();
                verileriCek();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
        }
    


        private void guncelleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var secili = (DataRowView)guncelleGrid.SelectedItem;
                guncelle1.Text = secili[0].ToString();
                guncelle2.Text = secili[1].ToString();
               Guncelle3.Text = secili[2].ToString();
                guncelle4.Text = secili[3].ToString();
                guncelle5.Text = secili[4].ToString();
                guncelle6.Text = secili[5].ToString();
                guncelle7.Text = secili[6].ToString();



            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void sorgulamaaa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            sorgulamaaa.Text = "";
        }

        private void sil_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                baglanti.Open();
               
                var secili = (DataRowView)izgara2.SelectedItem;
                
                OleDbCommand komut = new OleDbCommand("DELETE FROM kargo WHERE Takip_Kodu=@tkod", baglanti);
            
                komut.Parameters.AddWithValue("@tkod", secili[0]);
             
                var sonuc = MessageBox.Show("Silmek istiyor musunuz?", "Silme onayı", MessageBoxButton.YesNo);
                if (sonuc == MessageBoxResult.Yes)
                {
                    
                    komut.ExecuteNonQuery();
                }
               
               

                baglanti.Close();
                verileriCek();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

       public void bosil()
        {
            
            
            guncelleGrid.Items.RemoveAt(guncelleGrid.Items.Count);
            izgara2.Items.RemoveAt(guncelleGrid.Items.Count);
        }
      
     
    }
}

//  String sql = "UPDATE Çalışanlar SET Takip_Kodu=@No, Gönderen=@gönderen, Gönderen_adresi=@gadres, Gönderen_telefon=@gtelefon, Alıcı=@alıcı, Alıcı_adres=@aadres, Alıcı_telefon=@atelefon WHERE Takip_Kodu=@No";


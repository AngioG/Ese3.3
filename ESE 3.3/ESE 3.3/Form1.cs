using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESE_3._3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Lezione[] eleL = new Lezione[1000];
        int num = 0;

        private void Inserisci(object sender, EventArgs e)
        {
            foreach (var txt in tabPage1.Controls)
            {
                if (txt.GetType().ToString() == "TextBox")
                    if (String.IsNullOrWhiteSpace((txt as TextBox).Text))
                    {
                        MessageBox.Show("Non hai inserito un dato", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }

            Lezione nuovo = default;

            nuovo.codice = txt_ins_codice.Text;
            nuovo.auto = txt_ins_auto.Text;

            if (decimal.TryParse(txt_ins_prezzo.Text, out decimal p))
                nuovo.costo = p;
            else
            {
                MessageBox.Show("Il prezzo non è un dato", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nuovo.data = ins_data.Value;
            if (nuovo.data < DateTime.Today)
            {
                MessageBox.Show("La data dell'esame deve essere antecedente a quella odierna", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nuovo.nome = txt_ins_nom.Text;
            if (!nuovo.nome.Contains(' '))
            {
                MessageBox.Show("Devono essere presenti sia il nome che il cognome", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nuovo.tipo = cmb_ins.SelectedItem.ToString();

            if (int.TryParse(txt_ins_Oinizio.Text, out int oi))
                nuovo.inizio.ore = oi;
            else
            {
                MessageBox.Show("L'ora di inizio deve essere un numero", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nuovo.inizio.ore < 7 || nuovo.inizio.ore > 19)
            {
                MessageBox.Show("L'ora di inizio deve essere tra le 7 e le 19", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (int.TryParse(txt_ins_Minizio.Text, out int mi))
                nuovo.inizio.minuti = mi;
            else
            {
                MessageBox.Show("I minuti di inizio devono essere un numero", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nuovo.inizio.minuti < 0 || nuovo.inizio.minuti > 59)
            {
                MessageBox.Show("Devi inserire minuti validi", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (int.TryParse(txt_ins_Ofine.Text, out int of))
                nuovo.fine.ore = of;
            else
            {
                MessageBox.Show("L'ora di fine deve essere un numero", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nuovo.fine.ore < 8 || nuovo.fine.ore > 20)
            {
                MessageBox.Show("L'ora di inizio deve essere tra le 8 e le 20", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (int.TryParse(txt_ins_Mfine.Text, out int mf))
                nuovo.fine.minuti = mf;
            else
            {
                MessageBox.Show("I minuti di fine devono essere un numero", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nuovo.fine.minuti < 0 || nuovo.fine.minuti > 59)
            {
                MessageBox.Show("Devi inserire minuti validi", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            eleL[num] = nuovo;
            num += 1;
            MessageBox.Show("Lezione aggiunta correttamente all'elenco", "Operazione riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);

            foreach (var txt in tabPage1.Controls)
            {
                if (txt.GetType().ToString() == "TextBox")
                    (txt as TextBox).Text = "";
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                listView1.Items.Clear();

                if (rad_ordina_cliente.Checked)
                    Class1.OrdinaNom(num, eleL);
                if (rad_ord_data.Checked)
                    Class1.OrdinaDat(num, eleL);

                for (int i = 0; i < num; i++)
                {
                    ListViewItem riga = new ListViewItem(new string[]
                    {
                        eleL[i].codice,
                        eleL[i].nome,
                        eleL[i].data.ToString("dd/MM/yy"),
                        ($"{eleL[i].inizio.ore.ToString("00")}:{eleL[i].inizio.minuti.ToString("00")}"),
                        ($"{eleL[i].fine.ore.ToString("00")}:{eleL[i].fine.minuti.ToString("00")}"),
                        eleL[i].auto,
                    });
                    listView1.Items.Add(riga);
                }

            }
        }

        private void src_edit(object sender, EventArgs e)
        {
            int i = Class1.cercaCod(num, eleL, txt_src_edit.Text);

            if (i == -1)
            {
                MessageBox.Show("Codice non trovato", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txt_edit_cod.Text = eleL[i].codice;
            txt_edit_auto.Text = eleL[i].auto;

            txt_edit_Oinizio.Text = eleL[i].inizio.ore.ToString("00");
            txt_edit_Minizio.Text = eleL[i].inizio.minuti.ToString("00");

            txt_edit_Ofine.Text = eleL[i].fine.ore.ToString("00");
            txt_edit_Mfine.Text = eleL[i].fine.minuti.ToString("00");

            txt_edit_prezzo.Text = eleL[i].costo.ToString("0.00");
            edit_dat.Value = eleL[i].data;

            cmb_edit_tipo.Text = eleL[i].tipo;

        }

        private void Edit(object sender, EventArgs e)
        {
            foreach (var txt in tabPage3.Controls)
            {
                if (txt.GetType().ToString() == "TextBox")
                    if (String.IsNullOrWhiteSpace((txt as TextBox).Text))
                    {
                        MessageBox.Show("Non hai editerito un dato", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }

            Lezione nuovo = default;

            nuovo.codice = txt_edit_cod.Text;
            nuovo.auto = txt_edit_auto.Text;

            if (decimal.TryParse(txt_edit_prezzo.Text, out decimal p))
                nuovo.costo = p;
            else
            {
                MessageBox.Show("Il prezzo non è un dato", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nuovo.data = edit_dat.Value;
            if (nuovo.data < DateTime.Today)
            {
                MessageBox.Show("La data dell'esame deve essere antecedente a quella odierna", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nuovo.nome = txt_edit_nom.Text;
            if (!nuovo.nome.Contains(' '))
            {
                MessageBox.Show("Devono essere presenti sia il nome che il cognome", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nuovo.tipo = cmb_edit_tipo.SelectedItem.ToString();

            if (int.TryParse(txt_edit_Oinizio.Text, out int oi))
                nuovo.inizio.ore = oi;
            else
            {
                MessageBox.Show("L'ora di inizio deve essere un numero", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nuovo.inizio.ore < 7 || nuovo.inizio.ore > 19)
            {
                MessageBox.Show("L'ora di inizio deve essere tra le 7 e le 19", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (int.TryParse(txt_edit_Minizio.Text, out int mi))
                nuovo.inizio.minuti = mi;
            else
            {
                MessageBox.Show("I minuti di inizio devono essere un numero", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nuovo.inizio.minuti < 0 || nuovo.inizio.minuti > 59)
            {
                MessageBox.Show("Devi editerire minuti validi", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (int.TryParse(txt_edit_Ofine.Text, out int of))
                nuovo.fine.ore = of;
            else
            {
                MessageBox.Show("L'ora di fine deve essere un numero", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nuovo.fine.ore < 8 || nuovo.fine.ore > 20)
            {
                MessageBox.Show("L'ora di inizio deve essere tra le 8 e le 20", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (int.TryParse(txt_edit_Mfine.Text, out int mf))
                nuovo.fine.minuti = mf;
            else
            {
                MessageBox.Show("I minuti di fine devono essere un numero", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nuovo.fine.minuti < 0 || nuovo.fine.minuti > 59)
            {
                MessageBox.Show("Devi editerire minuti validi", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Class1.cercaCod(num, eleL, txt_edit_cod.Text);

            MessageBox.Show("Lezione modificata correttamente", "Operazione riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);

            foreach (var txt in tabPage3.Controls)
            {
                if (txt.GetType().ToString() == "TextBox")
                    (txt as TextBox).Text = "";
            }
        }

        private void Elimina(object sender, EventArgs e)
        {
            int found = Class1.Elimina(ref num, eleL, cmb_del.Text);

            if (found == 0)
                MessageBox.Show("Nessuna lezione corrispondente a questo tipo di patente", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show($"{found} elementi rimossi correttamente", "Operazione riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Somma(object sender, EventArgs e)
        {
            decimal somma = 0;
            int lezioni = 0;

            for (int x = 0; x < num; x++)
                if (eleL[x].nome == txt_somma_nom.Text)
                {
                    somma += eleL[x].costo;
                    lezioni += 1;
                }

            if (lezioni != 0)
                lbl_somma.Text = $"{txt_somma_nom.Text} ha prenotato {lezioni} lezioni,\nper un totale di {somma} euro";
            else
                lbl_somma.Text = $"{txt_somma_nom.Text} non ha nessuna lezione programmata";
        }

        private void Media_date(object sender, EventArgs e)
        {
            DateTime inizio = date_inizio.Value;
            DateTime fine = date_fine.Value;

            if (inizio >= fine)
                MessageBox.Show("La data di inizio deve essere miore di quella di fine", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);

            int x = 0;

            int media = 0;
            int b = 0;

            while (x < num)
            {
                if (eleL[x].data > inizio && eleL[x].data < fine)
                {
                    int min1 = eleL[x].inizio.minuti + (eleL[x].inizio.ore * 60);
                    int min2 = eleL[x].fine.minuti + (eleL[x].fine.ore * 60);

                    media += min2 - min1;

                    b += 1;
                }
                x++;
            }

            if (b == 0)
            {
                MessageBox.Show("Nessuna lezione in questo periodo di tempo", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lbl_date.Visible = true;
            media = media / b;
            lbl_date.Text = $"In questo periodo ci sono {b} lezioni, che durano in media {media} minuti";

        }

        private void cambio(object sender, EventArgs e)
        {
            lbl_date.Visible = false;
        }

        private void Tardi(object sender, EventArgs e)
        {
            orario tardi = default;

            tardi.ore = 0;
            tardi.minuti = 0;



            int i = 0;
            while (i < num)
            {
                if (eleL[i].nome == txt_src_tardi.Text)
                {
                    bool maggiore = Class1.ConfrontaOrari(tardi, eleL[i].inizio);
                    if (maggiore)
                        tardi = eleL[i].inizio;
                }

                i += 1;

            }

            if (i == num)
            {
                MessageBox.Show("Nome non trovato", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            txt_tardi_cod.Text = eleL[i].codice;
            txt_tardi_auto.Text = eleL[i].auto;

            txt_tardi_Oinizio.Text = eleL[i].inizio.ore.ToString("00");
            txt_tardi_Minizio.Text = eleL[i].inizio.minuti.ToString("00");

            txt_tardi_Ofine.Text = eleL[i].fine.ore.ToString("00");
            txt_tardi_Mfine.Text = eleL[i].fine.minuti.ToString("00");

            txt_tardi_prezzo.Text = eleL[i].costo.ToString("0.00");
            tardi_data.Value = eleL[i].data;

            cmb_tardi_tipo.Text = eleL[i].tipo;
        }

        private void punto_nove(object sender, EventArgs e)
        {
            DateTime inizio = date_ini.Value;
            DateTime fine = date_fin.Value;

            if (inizio >= fine)
                MessageBox.Show("La data di inizio deve essere miore di quella di fine", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Class1.OrdinaDat(num, eleL);

            int i = 0;

            while (i < num)
            {
                ListViewItem riga = default;

                if (eleL[i].data > inizio && eleL[i].data < fine)
                {
                    riga = new ListViewItem(new string[]{
                        eleL[i].codice,
                        eleL[i].nome,
                        eleL[i].data.ToString("dd/MM/yy"),
                        ($"{eleL[i].inizio.ore.ToString("00")}:{eleL[i].inizio.minuti.ToString("00")}"),
                        ($"{eleL[i].fine.ore.ToString("00")}:{eleL[i].fine.minuti.ToString("00")}"),
                        eleL[i].auto,
                    });
                    listView2.Items.Add(riga);
                }
                i++;
            }
        }

        private void Precaricamento(object sender, EventArgs e)
        {
            eleL[num].codice = "A000";
            eleL[num].nome = "Mario Rossi";
            eleL[num].data = DateTime.Parse("5/7/2021");
            eleL[num].inizio.ore = 19;
            eleL[num].inizio.minuti = 00;
            eleL[num].fine.ore = 20;
            eleL[num].fine.minuti = 00;
            eleL[num].auto = "Fiat 500";
            eleL[num].tipo = "A";
            eleL[num].costo = 50.00m;
            num += 1;

            eleL[num].codice = "A001";
            eleL[num].nome = "Mario Rossi";
            eleL[num].data = DateTime.Parse("24/6/2021");
            eleL[num].inizio.ore = 18;
            eleL[num].inizio.minuti = 30;
            eleL[num].fine.ore = 20;
            eleL[num].fine.minuti = 00;
            eleL[num].auto = "Fiat 500";
            eleL[num].tipo = "A";
            eleL[num].costo = 50.00m;
            num += 1;

            eleL[num].codice = "A002";
            eleL[num].nome = "Filippo Bianchi";
            eleL[num].data = DateTime.Parse("7/8/2021");
            eleL[num].inizio.ore = 16;
            eleL[num].inizio.minuti = 00;
            eleL[num].fine.ore = 17;
            eleL[num].fine.minuti = 30;
            eleL[num].auto = "Pandino atomico";
            eleL[num].tipo = "B";
            eleL[num].costo = 45.00m;
            num += 1;

            eleL[num].codice = "A003";
            eleL[num].nome = "Filippo Bianchi";
            eleL[num].data = DateTime.Parse("25/7/2021");
            eleL[num].inizio.ore = 16;
            eleL[num].inizio.minuti = 00;
            eleL[num].fine.ore = 18;
            eleL[num].fine.minuti = 00;
            eleL[num].auto = "FIAT Panda";
            eleL[num].tipo = "B";
            eleL[num].costo = 37.00m;
            num += 1;

            eleL[num].codice = "A004";
            eleL[num].nome = "Viola Rota";
            eleL[num].data = DateTime.Parse("15/6/2021");
            eleL[num].inizio.ore = 8;
            eleL[num].inizio.minuti = 00;
            eleL[num].fine.ore = 9;
            eleL[num].fine.minuti = 30;
            eleL[num].auto = "Fiat 500";
            eleL[num].tipo = "A";
            eleL[num].costo = 50.00m;
            num += 1;

        }

        private void Usata(object sender, EventArgs e)
        {
            auto usata = Class1.Usata(num, eleL);
            label43.Text = $"L'auto più usata è una {usata.nome},\nche viene usata in {usata.usi} lezioni";
        }
    }
}

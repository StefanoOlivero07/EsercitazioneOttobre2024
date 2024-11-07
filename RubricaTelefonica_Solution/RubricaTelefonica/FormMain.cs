using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubricaTelefonica
{
    public partial class FormMain : Form
    {
        List<Contatto> contatti = new List<Contatto>();
        bool modificato = false;
        string nome = "";
        string telefono = "";

        public FormMain()
        {
            InitializeComponent();
        }

        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string telefono = txtTelefono.Text;

            Contatto persona = Contatto.creaContatto(nome, telefono, contatti);
            if (persona != null )
            {
                svuotaTextbox();
                contatti.Add(persona);
                visualizzaContatti();
            }
        }

        private void visualizzaContatti()
        {
            lstContatti.Items.Clear();
            foreach (Contatto contatto in contatti)
                lstContatti.Items.Add(contatto);
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if (lstContatti.SelectedIndex >= 0)
            {
                Contatto contattoDaEliminare = (Contatto)lstContatti.SelectedItem;
                contatti.RemoveRange(contatti.IndexOf(contattoDaEliminare), 1);
                svuotaTextbox();
                visualizzaContatti();
            }
            else if (lstContatti.Items.Count == 0)
                MessageBox.Show("Rubrica vuota", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Seleziona un contatto", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {

            if (lstContatti.Items.Count > 0)
            {
                btnAggiungi.Enabled = false;
                btnElimina.Enabled = false;
                btnCarica.Enabled = false;
                btnSalva.Enabled = false;

                if (modificato == false)
                {
                    lblModifica.Visible = true;
                    modificato = true;
                }
                else
                {
                    if (txtNome.Text != "" && txtTelefono.Text != "")
                    {
                        if (txtNome.Text != nome || txtTelefono.Text != telefono)
                        {
                            Contatto contattoModificato = (Contatto)lstContatti.SelectedItem;
                            //Contatto modificato = Contatto.creaContatto(txtNome.Text, txtTelefono.Text, contatti);
                            if (contattoModificato != null)
                            {
                                contattoModificato.nome = txtNome.Text;
                                contattoModificato.telefono = txtTelefono.Text;
                                //int index = 0;
                                //while (contatti[index].nome != nome || contatti[index].telefono != telefono)
                                //    index++;
                                
                                //contatti[index] = modificato;
                                visualizzaContatti();
                            }
                        }
                        lblModifica.Visible = false;
                        btnAggiungi.Enabled = true;
                        btnElimina.Enabled = true;
                        btnCarica.Enabled = true;
                        btnSalva.Enabled = true;
                        modificato = false;
                    }
                    else
                        MessageBox.Show("Selezionare un contatto", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                svuotaTextbox();
            }
            else
                MessageBox.Show("Rubrica vuota", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void lstContatti_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modificato == true)
            {
                nome = lstContatti.SelectedItem.ToString().Split('-')[0].Trim();
                telefono = lstContatti.SelectedItem.ToString().Split('-')[1].Trim();

                txtNome.Text = nome;
                txtTelefono.Text = telefono;
            }
        }

        private void svuotaTextbox()
        {
            txtNome.Text = "";
            txtTelefono.Text = "";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            lstContatti.Items.Clear();
            string text = txtSearch.Text;

            foreach (Contatto contatto in contatti)
            {
                if (contatto.nome.Contains(text) || contatto.telefono.Contains(text))
                    lstContatti.Items.Add(contatto);
            }
        }

        private void btnCarica_Click(object sender, EventArgs e)
        {
            contatti.Clear();

            StreamReader sr = new StreamReader("rubrica.txt");
            string[] s;
            Contatto persona;

            while (!sr.EndOfStream)
            {
                s = sr.ReadLine().Split(',');
                persona = Contatto.creaContatto(s[0], s[1], contatti);
                if (persona != null)
                    contatti.Add(persona);
            }
            sr.Close();
            visualizzaContatti();
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("rubrica.txt", false);

            foreach (Contatto contatto in contatti)
                sw.Write(contatto.nome + "," + contatto.telefono + "\n");
            sw.Close();
            MessageBox.Show("File salvato correttamente", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

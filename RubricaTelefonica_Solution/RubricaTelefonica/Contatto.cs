using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubricaTelefonica
{
    internal class Contatto
    {
        public string nome;
        public string telefono;

        private Contatto (string n, string t)
        {
            nome = n;
            telefono = t;
        }

        public static Contatto creaContatto (string n, string t, List<Contatto> contatti)
        {
            n = n.Trim();
            t = t.Trim();

            if (n.Length > 0 && t.Length > 0)
            {
                foreach (Contatto contatto in contatti)
                {
                    if (contatto.nome == n && contatto.telefono == t)
                    {
                        MessageBox.Show("Contatto già esistente", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
                return new Contatto(n, t);
            }
            MessageBox.Show("Riempire tutti i campi", "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        public override string ToString()
        {
            return $"{nome} - {telefono}";
        }
    }
}

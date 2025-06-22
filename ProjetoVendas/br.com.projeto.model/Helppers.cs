using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoVendas.br.com.projeto.model
{
    public static class Helppers
    {
        public static void LimparCampos(Control controlePai)
        {
            foreach (Control ctrl in controlePai.Controls)
            {
                if (ctrl is TextBox txt)
                    txt.Clear();
                else if (ctrl is MaskedTextBox mtb)
                    mtb.Clear();
                else if (ctrl is ComboBox cb)
                    cb.SelectedIndex = -1;
                else if (ctrl.HasChildren)
                    LimparCampos(ctrl); 
            }
        }
    }
}

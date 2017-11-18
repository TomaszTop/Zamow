using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZAMOW
{
   public static class ErrorBox
    {
        public static void Show(Exception ex)
        {
            MessageBox.Show(ex.Message, "BŁĄD!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

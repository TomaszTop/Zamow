using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZAMOW
{
    public partial class Form1 : Form
    {

        Database database = null;
        public Form1()
        {
            InitializeComponent();
        }
        DataTable dt = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                database = new Database();
            }
            catch(Exception ex)
            {
                ErrorBox.Show(ex);
                bSend.Enabled = false;
                return;
            }

            LoadTable();
        }

        private void LoadTable()
        {
            try
            {
                dt = database.GetItems();
                dt.Columns.Add("zamow");
                dgvItems.DataSource = dt;
                dgvItems.Columns[0].Visible = false;
                dgvItems.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvItems.Columns[1].HeaderText = "NAZWA PRODUKTU";
                dgvItems.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvItems.Columns[2].HeaderText = "OSTATNIO ZAMAWIANA ILOŚĆ";
                dgvItems.Columns[3].HeaderText = "ZAMÓWIENIE";
            }
            catch (MyException ex)
            {
                ErrorBox.Show(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string mailBody = "";

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        mailBody += row.ItemArray[i].ToString() + "\t";
                    }
                    mailBody += Environment.NewLine;
                }

                Mail mail = new Mail("smtp-mail.outlook.com", "tomaszfilipek@outlook.com", "tomaszfilipek@outlook.com", "tOMASZ1pOCZTA2016", true, 587);
                mail.SendMail("tomaszfilipek@outlook.com", "Zamowienie " + DateTime.Now.ToShortDateString(), mailBody);
            }

            catch (MyException ex)
            {
                ErrorBox.Show(ex);
                return;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {

                    if (row["zamow"] == DBNull.Value) continue;

                    int id = Convert.ToInt32(row["id"]);
                    int order = Convert.ToInt32(row["zamow"]);

                    database.UpdateItemLastOrder(id, order);                   
                }
            }
            catch (MyException ex)
            {
                ErrorBox.Show(ex);
            }

            LoadTable();
        }
    }
}

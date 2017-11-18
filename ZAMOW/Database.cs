using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace ZAMOW
{
    public class Database
    {
        private SQLiteConnection sQLiteConnection = null;

        public Database()
        {
            string dataBaseFilePath = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "database.db");

              if (!File.Exists(dataBaseFilePath))
            throw new Exception("Brak pliku bazy danych");          

            try
            {
                sQLiteConnection = new SQLiteConnection(@"Data Source="+ dataBaseFilePath + ";Version=3;");
                sQLiteConnection.Open();
            }
            catch (Exception ex)
            {
                throw new MyException(ex, "Połączenie z bazą danych nieudane");                
            }
        }

        internal DataTable GetItems()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteDataAdapter sQLiteDataadapter = new SQLiteDataAdapter("SELECT * FROM towary", sQLiteConnection))
                {
                    sQLiteDataadapter.Fill(dt);
                }
                return dt;
            }
            catch(Exception ex)
            {
                throw new MyException(ex, "Pobieranie produktów nieudane");
            }

        }

        internal void UpdateItemLastOrder(int id, int order)
        {
            try
            {
                using (SQLiteCommand sqlCommand = new SQLiteCommand("UPDATE towary SET ostatniezamowienie = @oz WHERE id = @id", sQLiteConnection))
            {
                sqlCommand.Parameters.AddWithValue("@oz", order);
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.ExecuteNonQuery();

            }
            }
            catch (Exception ex)
            {
                throw new MyException(ex, "Pobieranie aktualizacji rekordów");
            }
        }
    }
}

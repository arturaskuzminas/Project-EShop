using Laikrodziai.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Laikrodziai.Repositories
{
    public class KlientasRepository
    {
        public List<KlientasModel> getKlientai()
        {
            List<KlientasModel> klientai = new List<KlientasModel>();
            string sqlquery = "select * from klientas";

            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;port=3306;database=laikrodziai;user=root;password=;");
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);

            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                klientai.Add(new KlientasModel
                {
                    ID = Convert.ToInt32(item["ID_numeris"]),
                    Vardas = Convert.ToString(item["vardas"]),
                    Pavarde = Convert.ToString(item["pavarde"]),
                    Adresas = Convert.ToString(item["adresas"]),
                    Email = Convert.ToString(item["e_pastas"]),
                    TelNr = Convert.ToInt64(item["telefono_numeris"]),
                    Slaptazodis = Convert.ToString(item["slaptazodis"]),
                    RegData = Convert.ToDateTime(item["registracijos_data"]),
                    Miestas = Convert.ToString(item["miestas"])
                });
            }
            return klientai;
        }
    }
}

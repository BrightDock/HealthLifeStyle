using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copytables
{
    class TCopy
    {
        TCopy() {

        }

        public void copy_tables()
        {
            OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=zap.mdb;Jet OLEDB:Database Password=BAngal");
            cn.Open();
            OleDbCommand com = new OleDbCommand("CREATE TABLE Результаты (номер COUNTER CONSTRAINT номер PRIMARY KEY,Тема STRING, Дата string ,Группа STRING, Студент STRING  ,Верных_ответов STRING,Результат STRING )", cn);
            com.ExecuteNonQuery();
            cn.Close();
        }
    }
}

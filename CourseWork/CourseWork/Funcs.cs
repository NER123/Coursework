using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public class Funcs
    {
        public static int OneYearPlan(string id, SqlConnection _conn)
        {
            int summ = 0;
            int temporate = 0;

            var cmds = new SqlCommand("SELECT SUM(Pay) AS Total FROM [Empl] WHERE UserID ='" + id + "'", _conn);
            var reader = cmds.ExecuteReader();

            reader.Read();

            if (reader.HasRows)
                temporate = reader.GetInt32(0) * 12;

            reader.Close();

            summ += temporate;

            cmds = new SqlCommand("SELECT Rent, Repair FROM [WorkPlacePay] WHERE UserID ='" + id + "'", _conn);
            reader = cmds.ExecuteReader();

            reader.Read();

            if (reader.HasRows)
                temporate = reader.GetInt32(0) * 12 + reader.GetInt32(1);

            reader.Close();

            summ += temporate;

            cmds = new SqlCommand("SELECT Cost, PerYear FROM [PlanRep] WHERE UserID ='" + id + "'", _conn);
            reader = cmds.ExecuteReader();
            temporate = 0;

            if (reader.HasRows)
                while (reader.Read())
                    temporate += reader.GetInt32(0) * reader.GetInt32(1) / 12;

            reader.Close();

            summ += temporate;

            return summ;
        }
    }
}

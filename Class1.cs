using System;
using System.Data.SqlClient;

namespace AdoBankLibraryConn
{
    public class BankConn
    {
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlDataReader dr;

        public string AddAccount(dynamic obj)
        {
            con = getCon();

            cmd = new SqlCommand("Insert into SBAccount values(@AccountNumber, @CustomerName, @CustomerAddress, @CurrentBalance)");

            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@AccountNumber", obj.AccountNumber);
            cmd.Parameters.AddWithValue("@CustomerName", obj.CustomerName);
            cmd.Parameters.AddWithValue("@CustomerAddress", obj.CustomerAddress);
            cmd.Parameters.AddWithValue("@CurrentBalance", obj.CurrentBalance);

            cmd.ExecuteNonQuery(); //insert, update, delete

            return "Record Added";

        }


        public static SqlConnection getCon()
        {
            //add your connection details as per your system
            con = new SqlConnection("Data Source=.; Initial Catalog=BankLibrary; Integrated Security=true");

            con.Open();

            return con;
        }

        public SqlDataReader GetParticularData(int AccountNumber)
        {
            con = getCon();
            
            cmd = new SqlCommand("select * from SBAccount where AccountNumber = @AccountNumber");
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@AccountNumber", AccountNumber);

            dr = cmd.ExecuteReader();

            return dr;

        }

        public SqlDataReader GetAllData()
        {
            con = getCon();

            cmd = new SqlCommand("select * from SBAccount");
            cmd.Connection = con;

            dr = cmd.ExecuteReader();

            return dr;

        }

        public static decimal getBalance(int accno)
        {
            con = getCon();

            cmd = new SqlCommand("select * from SBAccount where AccountNumber = @AccountNumber");
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@AccountNumber", accno);

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                return Convert.ToDecimal(dr[3]);
            }

            return 0;
        }

        public string DepositMoney(int accno, decimal amt)
        {
            decimal newamt = getBalance(accno) + amt;

            try
            {
                con = getCon();
                cmd = new SqlCommand("update SBAccount set CurrentBalance = @CurrentBalance where AccountNumber = @AccountNumber");
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@AccountNumber", accno);
                cmd.Parameters.AddWithValue("@CurrentBalance", newamt);
                
                string msg = cmd.ExecuteNonQuery().ToString();
                return msg;
            }
            catch (SqlException s)
            {
                return s.Message;
            }
            finally
            {
                con.Dispose();
            }


        }


        public string WithdrawMoney(int accno, decimal amt)
        {
            if (amt <= getBalance(accno))
            {
                decimal newamt = getBalance(accno) - amt;

                try
                {
                    con = getCon();
                    cmd = new SqlCommand("update SBAccount set CurrentBalance = @CurrentBalance where AccountNumber = @AccountNumber");
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@AccountNumber", accno);
                    cmd.Parameters.AddWithValue("@CurrentBalance", newamt);

                    string msg = cmd.ExecuteNonQuery().ToString();
                    return msg;
                }
                catch (SqlException s)
                {
                    return s.Message;
                }
                finally
                {
                    con.Dispose();
                }
            }

            return "Balance insufficient!";

        }



        public string AddTransaction(dynamic obj)
        {
            con = getCon();

            cmd = new SqlCommand("Insert into SBTransaction(TransactionDate, AccountNumber, Amount, TransactionType) values(@TransactionDate, @AccountNumber, @Amount, @TransactionType)");

            cmd.Connection = con;

            
            cmd.Parameters.AddWithValue("@TransactionDate", obj.TransactionDate);
            cmd.Parameters.AddWithValue("@AccountNumber", obj.AccountNumber);
            cmd.Parameters.AddWithValue("@Amount", obj.Amount);
            cmd.Parameters.AddWithValue("@TransactionType", obj.TransactionType);

            cmd.ExecuteNonQuery(); //insert, update, delete

            return "Record Added";

        }

        public SqlDataReader GetParticularTran(int accno)
        {
            con = getCon();

            cmd = new SqlCommand("select * from SBTransaction where AccountNumber = @AccountNumber");
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@AccountNumber", accno);

            dr = cmd.ExecuteReader();

            return dr;

        }








    }
}

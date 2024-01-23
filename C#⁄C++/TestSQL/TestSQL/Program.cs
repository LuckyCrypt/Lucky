using System;
using System.Data.SqlClient;

namespace TestSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] grafik = new double[12, 5];
            string connectionString = "Server=DESKTOP-UT5RF0P\\SQLEXPRESS;Database=ZadanieTest;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(connectionString);
            using (con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Подключение открыто");
                    string select = "SELECT* FROM RequestParametersAll1 where ID_CLient = 1";
                    SqlCommand command = new SqlCommand(select, con);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    string Mount = reader.GetString(reader.GetOrdinal("LoanTerm"));
                    decimal CreditSumma = reader.GetDecimal(reader.GetOrdinal("CreditAmount"));
                    double Procent = reader.GetDouble(reader.GetOrdinal("LendingRate"));
                    int IntMount = Int32.Parse(Mount);
                    double CreditSummaDouble = (double)CreditSumma;
                    grafik = (double[,])ResizeArray(grafik, new int[] { IntMount, 5 });
                    double ProcentStavka = Procent / (100 * 12);
                    Payment(CreditSummaDouble, Procent, IntMount);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.ReadKey();
        }
        private static Array ResizeArray(Array arr, int[] newSizes)
        {
            if (newSizes.Length != arr.Rank)
                throw new ArgumentException("arr must have the same number of dimensions " +
                                            "as there are elements in newSizes", "newSizes");
            var temp = Array.CreateInstance(arr.GetType().GetElementType(), newSizes);
            int length = arr.Length <= temp.Length ? arr.Length : temp.Length;
            Array.ConstrainedCopy(arr, 0, temp, 0, length);
            return temp;
        }
        private static void Payment(double credit, double rate, int month)
        {
            Console.WriteLine("*************Ежемесячные платежи будут такие:************************");
            double monthpay, ratem, monthproc, credbody, creditcopy;
            creditcopy = credit;
            int ii = 0;
            int i = 1;
            Console.WriteLine("Месяц" + "\t\t" + "Платеж: " + "\t\t\t" + "Долг: " + "\t\t\t" + "Проценты: " + "\t\t" + "Остаток: ");
            ratem = rate / (100 * 12);
            int mount1 = month * -1;
            monthpay = credit * (ratem + (ratem / (Math.Pow(1 + ratem, month) - 1)));
            while (month > ii)
            {

                monthproc = credit * ratem;

                credit = credit - (monthpay - monthproc);
                credbody = monthpay - monthproc;
                if (creditcopy > monthpay)
                {
                    PrintData(i, monthpay, credbody, monthproc, credit);
                }
                else
                {
                    PrintData(i, credbody, credbody, monthproc, credit);
                }
                i++;
                ii++;
                creditcopy = credit;
            }
        }
        static void PrintData(int i, double mp, double cb, double mpc, double cr)
        {
            Console.WriteLine("{0}" + "\t\t" + "{1}" + "\t\t\t" + "{2}" + "\t\t" + "{3}" + "\t\t\t" + "{4}", i, Math.Round(mp, 2), Math.Round(cb, 2), Math.Round(mpc, 2), Math.Round(cr, 2));
        }
    }

}

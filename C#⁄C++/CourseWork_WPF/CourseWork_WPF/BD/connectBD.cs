using System.Data.SqlClient;

namespace CourseWork_WPF
{
    internal class connectBD
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-33Q2LF7; Initial Catalog = MapSearch; Integrated Security = True");

        public void OpenConnections()
        {

            if (connectionString.State == System.Data.ConnectionState.Closed)
            {
                connectionString.Open();
            }
        }
        public void CloseConnections()
        {
            if (connectionString.State == System.Data.ConnectionState.Open)
            {
                connectionString.Close();
            }
        }
        public SqlConnection GetConnections()
        {
            return connectionString;
        }

    }
}

using System.Data;
using System.Data.SqlClient;

namespace WiiController
{
    public class SqlHelpers
    {
        public static int CommandTimeOut { get; set; }
        private static int DefaultTimeOut = 30;

        public static void ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandTimeout = CommandTimeOut == 0 ? DefaultTimeOut : CommandTimeOut;
            command.CommandType = commandType;
            command.CommandText = commandText;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static void ExecuteNonQuery(SqlConnection sqlConnection, CommandType commandType, string commandText)
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandTimeout = CommandTimeOut == 0 ? DefaultTimeOut : CommandTimeOut;
            command.CommandType = commandType;
            command.CommandText = commandText;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static void ExecuteNonQuery(SqlTransaction sqlTransaction, CommandType commandType, string commandText)
        {
            SqlCommand command = new SqlCommand();
            command.Transaction = sqlTransaction;
            command.Connection = sqlTransaction.Connection;
            command.CommandTimeout = CommandTimeOut == 0 ? DefaultTimeOut : CommandTimeOut;
            command.CommandType = commandType;
            command.CommandText = commandText;
            command.ExecuteNonQuery();
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            DataSet dataSet = new DataSet();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandTimeout = CommandTimeOut == 0 ? DefaultTimeOut : CommandTimeOut;
            command.CommandType = commandType;
            command.CommandText = commandText;
            dataSet = ExecuteDataSet(command);
            sqlConnection.Close();
            return dataSet;
        }

        public static DataSet ExecuteDataset(SqlConnection sqlConnection, CommandType commandType, string commandText)
        {
            DataSet dataSet = new DataSet();
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandTimeout = CommandTimeOut == 0 ? DefaultTimeOut : CommandTimeOut;
            command.CommandType = commandType;
            command.CommandText = commandText;
            dataSet = ExecuteDataSet(command);
            sqlConnection.Close();
            return dataSet;
        }

        public static DataSet ExecuteDataset(SqlTransaction sqlTransaction, CommandType commandType, string commandText)
        {
            DataSet dataSet = new DataSet();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlTransaction.Connection;
            command.CommandTimeout = CommandTimeOut == 0 ? DefaultTimeOut : CommandTimeOut;
            command.CommandType = commandType;
            command.CommandText = commandText;
            dataSet = ExecuteDataSet(command);
            return dataSet;
        }


        private static DataSet ExecuteDataSet(SqlCommand sqlCommand)
        {
            var ds = new DataSet();
            using (var dataAdapter = new SqlDataAdapter(sqlCommand))
            {
                dataAdapter.Fill(ds);
            }

            return ds;
        }
    }
}

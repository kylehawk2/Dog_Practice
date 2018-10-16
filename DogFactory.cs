using System.Data;
using Dog_Practice.Models;
using MySql.Data.MySqlClient;
using Dapper;

namespace Dog_Practice
{
    public class DogFactory
    {
        static string server = "localhost";
        static string db = "dog"; //Change to your schema name
        static string port = "3306"; //Potentially 8889
        static string user = "root";
        static string pass = "root";
        internal IDbConnection Connection {
            get {
                return new MySqlConnection($"Server={server};Port={port};Database={db};UserID={user};Password={pass};SslMode=None");
            }
        }

        public Dog GetDogById(int dogId)
        {
            using(var dbConnection = Connection)
            {
                string SQL = "SELECT * FROM dogs WHERE dogId = @ID";
                return Connection.Query<Dog>(SQL, new {ID=dogId}).First();
            }
            

        }
    }
}
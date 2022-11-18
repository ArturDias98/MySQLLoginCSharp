using Login.Commom;
using Login.Commom.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Login.Service
{
    public class Service
    {
        private static MySqlConnection _connection;
        public Service()
        {

        }

        public static event Action<LoggerModel> OnServiceReport;

        public static async Task<bool> OpenConnection()
        {
            try
            {
                _connection = new MySqlConnection(Constants.Server);
                await _connection.OpenAsync();
                OnServiceReport?.Invoke(new LoggerModel { Message = "Connected to Database", Success = true });
                return true;
            }
            catch (Exception ex)
            {

                OnServiceReport?.Invoke(new LoggerModel { Message = ex.Message, Success = false });
            }
            return false;
        }
        public static async Task<AccountModel> CreateAccount(AccountModel model)
        {
            try
            {
                string cmd = "INSERT INTO users (user_name, user_email, user_password) " +
                    $"VALUES ('{model.UserName}', '{model.Email}', '{model.Password}')";

                var command = new MySqlCommand(cmd, _connection);
                var reader = await command.ExecuteReaderAsync();

                OnServiceReport?.Invoke(new LoggerModel { Message = "Account Created", Success = true});

                await reader.CloseAsync();

                return model;
            }
            catch (Exception ex)
            {

                OnServiceReport?.Invoke(new LoggerModel { Message = ex.Message, Success = false });
            }

            return null;
        }

        public static async Task<DatabaseModel> SelectAccount(AccountModel accountModel)
        {
            try
            {
                string cmd = $"SELECT * FROM users WHERE user_email = '{accountModel.Email}'" +
                    $"AND user_password = '{accountModel.Password}'";

                var command = new MySqlCommand(cmd, _connection);
                var reader = await command.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    OnServiceReport?.Invoke(new LoggerModel { Message = "User not found", Success = false });

                    await reader.CloseAsync();

                    return null;
                }

                var model = new DatabaseModel();

                while (await reader.ReadAsync())
                {
                    model.Id = Convert.ToInt32(reader["id"].ToString());
                    model.UserName = reader["user_name"].ToString();
                    model.Email = reader["user_email"].ToString();
                    model.Profile = reader["user_profile"].ToString();
                    model.FirstName = reader["user_first_name"].ToString();
                    model.LastName = reader["user_last_name"].ToString();
                    model.Password = reader["user_password"].ToString();
                }

                await reader.CloseAsync();

                return model;

            }
            catch (Exception ex)
            {
                OnServiceReport?.Invoke(new LoggerModel { Message = ex.Message, Success = false });

            }

            return null;
        }

        public static async Task<bool> UpdateAccount(DatabaseModel model)
        {
            try
            {
                string cmd = $"UPDATE users SET user_name = '{model.UserName}'," +
                    $"user_email = '{model.Email}', user_profile = '{model.Profile}'" +
                    $"WHERE id = {model.Id}";

                var command = new MySqlCommand(cmd, _connection);
                var reader = await command.ExecuteNonQueryAsync();

                OnServiceReport?.Invoke(new LoggerModel { Message = "Profile Updated", Success = true });

                return true;
                
            }
            catch (Exception ex)
            {
                OnServiceReport?.Invoke(new LoggerModel { Message = ex.Message, Success = false });
            }

            return false;
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Model;

namespace DataAccessLayer.DataBaseImpelemtation
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(IDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        private readonly IDBContext dBContext;

        public User GetUserByLoginPassword(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(dBContext.ConnectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    User user = null;
                    cmd.CommandText = "GetUser";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);
                    conn.Open();
                    try
                    {
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            user = new User();
                            user.UserId = int.Parse(dataReader["UserId"].ToString());
                            user.Name = dataReader["UserName"].ToString();
                            user.Login = dataReader["UserLogin"].ToString();
                            user.Password = dataReader["Password"].ToString();
                            user.Role = int.Parse(dataReader["RoleId"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    return user;
                }
            }
        }

        public int RegisterUser(User u)
        {
            using (SqlConnection conn = new SqlConnection(dBContext.ConnectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"insert into Users values(@Name,@Login,@Password,@Role)";
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Name", u.Name);
                    cmd.Parameters.AddWithValue("@Login", u.Login);
                    cmd.Parameters.AddWithValue("@Password", u.Password);
                    cmd.Parameters.AddWithValue("@Role", u.Role);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool CheckUserExsits(string nick)
        {
            using SqlConnection conn = new SqlConnection(dBContext.ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"select 1 from users where UserLogin = @Log";
                cmd.Parameters.AddWithValue("@Log", nick);
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

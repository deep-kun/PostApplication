using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Model;

namespace DataAccessLayer.DataBaseImpelemtation
{
    public class UserRepository : IUserRepository
    {
        private readonly IDBContext dBContext;

        public UserRepository(IDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public User GetUserByLoginPassword(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(dBContext.ConnectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    User user = null;
                    cmd.CommandText = "GetUserByLoginAndPassword";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@PasswordHash", password);
                    conn.Open();
                    try
                    {
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            user.UserId = int.Parse(dataReader["UserId"].ToString());
                            user.Name = dataReader["UserName"].ToString();
                            user.Login = dataReader["UserLogin"].ToString();
                            user.PasswordHash = dataReader["Password"].ToString();
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
            using SqlConnection conn = new SqlConnection(dBContext.ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"insert into Users values(@UserName, @UserLogin, @PasswordHash, @RoleId)";
                conn.Open();
                cmd.Parameters.AddWithValue("@UserName", u.Name);
                cmd.Parameters.AddWithValue("@UserLogin", u.Login);
                cmd.Parameters.AddWithValue("@PasswordHash", u.PasswordHash);
                cmd.Parameters.AddWithValue("@RoleId", u.Role);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public User GetUserByLogin(string nick)
        {
            using SqlConnection conn = new SqlConnection(dBContext.ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"select * from users where UserLogin = @Log";
                cmd.Parameters.AddWithValue("@Log", nick);
                conn.Open();
                var dataReader = cmd.ExecuteReader();
                
                if (!dataReader.Read())
                {
                    return null;
                }

                var user = new User();
                user.UserId = int.Parse(dataReader["UserId"].ToString());
                user.Name = dataReader["UserName"].ToString();
                user.Login = dataReader["UserLogin"].ToString();
                user.PasswordHash = dataReader["Password"].ToString();
                user.Role = int.Parse(dataReader["RoleId"].ToString());

                return user;
            }
        }
    }
}

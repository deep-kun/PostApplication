using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;
using DataAccesLayer.Model;
using System.Diagnostics;

namespace DataAccesLayer
{
    public class UserRepositiry : IUserRepositiry
    {
        string cons = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=PostService;Integrated Security=yes";

        public User GetUserByLoginPassword(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(cons))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    User user = new User();
                    cmd.CommandText = "GetUser";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);
                    conn.Open();
                    try
                    {
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        dataReader.Read();
                        user.UserId = Int32.Parse(dataReader["UserId"].ToString());
                        user.Name = dataReader["UserName"].ToString();
                        user.Login = dataReader["UserLogin"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.Role = Int32.Parse(dataReader["RoleId"].ToString());
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
            using (SqlConnection conn = new SqlConnection(cons))
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

        public List<Message> GetMessagesForUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(cons))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select Message.MessageId,Message.Date,Message.Subject,Users_Messages_Mapped.IsRead,Users_Messages_Mapped.PlaceHolderId,Users_Messages_Mapped.IsStarred,(select UserName from Users where Message.AuthorId=Users.UserId) as Author  from Users 
                            inner join Users_Messages_Mapped on Users.UserId=Users_Messages_Mapped.UserId
                            inner join Message on Users_Messages_Mapped.MessageId=Message.MessageId
                            where Users.UserId=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    try
                    {
                        SqlDataReader dataReader = cmd.ExecuteReader();

                        var list = new List<Message>();
                        Message m;

                        while (dataReader.Read())
                        {
                            m = new Message();
                            m.MessageId = Int32.Parse(dataReader["MessageId"].ToString());
                            m.Date = (DateTime)dataReader["Date"];
                            m.Subject = dataReader["Subject"].ToString();
                            m.IsRead = (bool)dataReader["IsRead"];
                            m.IsStared = (bool)dataReader["IsStarred"];
                            m.Author = dataReader["Author"].ToString();
                            list.Add(m);
                        }
                        return list;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }

        public MessageBody GetMessageById(int id)
        {
            using (SqlConnection conn = new SqlConnection(cons))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select Message.MessageId,Subject,Date,Body,(select UserName from Users where Users.UserId=AuthorId) as Author,Users_Messages_Mapped.UserId from Message 
inner join Users_Messages_Mapped on Message.MessageId=Users_Messages_Mapped.MessageId
where Message.MessageId=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    MessageBody mb = new MessageBody();
                    try
                    {
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        dataReader.Read();
                        mb.MessageId = Int32.Parse(dataReader["MessageId"].ToString());
                        mb.Subject = dataReader["Subject"].ToString();
                        mb.Date = (DateTime)dataReader["Date"];
                        mb.Body = dataReader["Body"].ToString();
                        mb.Author = dataReader["Author"].ToString();
                        mb.ReciverId= Int32.Parse(dataReader["UserId"].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return mb;
                }
            }
        }

        public void SetMessageRead(int id)
        {
            using (SqlConnection conn = new SqlConnection(cons))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Users_Messages_Mapped SET IsRead= 1 where Users_Messages_Mapped.MessageId=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveMsg(int id)
        {
            using (SqlConnection conn = new SqlConnection(cons))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"delete from Users_Messages_Mapped where Users_Messages_Mapped.MessageId=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        /*public void SendMsg(SendedMessage sendedMessage)
        {
            int mid;
            using (SqlConnection conn = new SqlConnection(cons))
            {

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"insert into Message values(@Subject,@Body,CURRENT_TIMESTAMP, @Author) SELECT SCOPE_IDENTITY()";
                    cmd.Parameters.AddWithValue("@Subject", sendedMessage.Subject);
                    cmd.Parameters.AddWithValue("@Body", sendedMessage.Body);
                    cmd.Parameters.AddWithValue("@Author", sendedMessage.AuthorId);
                    conn.Open();
                    mid = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            using (SqlConnection conn = new SqlConnection(cons))
            {

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"insert into Users_Messages_Mapped values(@MId,(select userid from users where userlogin=@UserLogin),1,0,0)";
                    cmd.Parameters.AddWithValue("@MId", mid);
                    cmd.Parameters.AddWithValue("@UserLogin", sendedMessage.Receiver);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
         
            }
        }*/
        public void SendMsg(SendedMessage sendedMessage)
        {
            using (SqlConnection connection = new SqlConnection(cons))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                cmd.Connection = connection;
                cmd.Transaction = transaction;
                int mid;
                try
                {
                    cmd.CommandText =
                        "insert into Message values(@Subject,@Body,CURRENT_TIMESTAMP, @Author) SELECT SCOPE_IDENTITY()";
                    cmd.Parameters.AddWithValue("@Subject", sendedMessage.Subject);
                    cmd.Parameters.AddWithValue("@Body", sendedMessage.Body);
                    cmd.Parameters.AddWithValue("@Author", sendedMessage.AuthorId);
                    mid = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.CommandText = @"insert into Users_Messages_Mapped values(@MId,(select userid from users where userlogin=@UserLogin),1,0,0)";
                    cmd.Parameters.AddWithValue("@MId", mid);
                    cmd.Parameters.AddWithValue("@UserLogin", sendedMessage.Receiver);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                    }
                }
            }
        }

        public bool CheckUser(string nick)
        {
            using (SqlConnection conn = new SqlConnection(cons))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select * from users where UserLogin = @Log";
                    cmd.Parameters.AddWithValue("@Log",nick);
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
}

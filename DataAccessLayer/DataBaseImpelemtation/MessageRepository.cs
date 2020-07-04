using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Model;

namespace DataAccessLayer.DataBaseImpelemtation
{
    public class MessageRepository : IMessageRepository
    {
        public MessageRepository(IDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        private readonly IDBContext dBContext;

        public IList<Message> GetMessagesForUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(dBContext.ConnectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            select Message.MessageId,Message.Date,Message.Subject,UsersMessagesMapped.IsRead,UsersMessagesMapped.PlaceHolderId,UsersMessagesMapped.IsStarred,(select UserName from Users where Messages.AuthorId=Users.UserId) as Author  from Users 
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
                            m.MessageId = int.Parse(dataReader["MessageId"].ToString());
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
                        return null;
                    }
                }
            }
        }

        public MessageBody GetMessageById(int id)
        {
            using (SqlConnection conn = new SqlConnection(dBContext.ConnectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    select Message.MessageId,
                        Subject,
                        Date,
                        Body,
                        (select UserName from Users where Users.UserId=AuthorId) as Author,
                        Users_Messages_Mapped.UserId 
                    from Message 
                        inner join Users_Messages_Mapped on Message.MessageId=Users_Messages_Mapped.MessageId
                        where Message.MessageId=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    MessageBody mb = new MessageBody();
                    try
                    {
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        dataReader.Read();
                        mb.MessageId = int.Parse(dataReader["MessageId"].ToString());
                        mb.Subject = dataReader["Subject"].ToString();
                        mb.Date = (DateTime)dataReader["Date"];
                        mb.Body = dataReader["Body"].ToString();
                        mb.Author = dataReader["Author"].ToString();
                        mb.ReceiverId = int.Parse(dataReader["UserId"].ToString());
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    return mb;
                }
            }
        }

        public void SetMessageRead(int id)
        {
            using SqlConnection conn = new SqlConnection(dBContext.ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    UPDATE Users_Messages_Mapped 
                    SET IsRead= 1
                    where Users_Messages_Mapped.MessageId=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveMsg(int id)
        {
            using SqlConnection conn = new SqlConnection(dBContext.ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    delete from Users_Messages_Mapped
                    where Users_Messages_Mapped.MessageId=@Id";
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

        public void SendMessage(SendedMessage sendedMessage)
        {
            using (SqlConnection connection = new SqlConnection(dBContext.ConnectionString))
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
    }
}

using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TicketsDao
    {
        public static int Add(Tickets ticket)
        {
            string sql = @"INSERT INTO T_TICKETS(TICKETTYPE, REMAINDER, BEGINNING, DESTINATION) 
                            VALUES (@TICKETTYPE, @REMAINDER, @BEGINNING, @DESTINATION)";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@TICKETTYPE",ticket.TicketType),
                new SqlParameter("@REMAINDER",ticket.Remainder),
                new SqlParameter("@BEGINNING",ticket.Beginning),
                new SqlParameter("@DESTINATION",ticket.Destination),
            };
            int res = SqlHelper.ExecuteNonQuery(sql, sqlParams);
            return res;
        }

        public static int CheckExist(string begin, string destination)
        {
            string sql = "SELECT COUNT(1) FROM T_TICKETS WHERE BEGINNING = @BEGINNING AND DESTINATION = @DESTINATION ";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@BEGINNING",begin),
                new SqlParameter("@DESTINATION",destination)
            };
            int res = SqlHelper.ExecuteScalar(sql, sqlParams);
            return res;
        }

        public static int Update(Tickets ticket)
        {
            string sql = @"UPDATE T_TICKETS SET REMAINDER=@REMAINDER
                            WHERE BEGINNING=@BEGINNING AND DESTINATION = @DESTINATION ";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@TICKETTYPE",ticket.TicketType),
                new SqlParameter("@REMAINDER",ticket.Remainder),
                new SqlParameter("@BEGINNING",ticket.Beginning),
                new SqlParameter("@DESTINATION",ticket.Destination),
            };
            int res = SqlHelper.ExecuteNonQuery(sql, sqlParams);
            return res;
        }

        /// <summary>
        /// 获取票列表
        /// </summary>
        /// <typeparam name="T">约束T为Tickets抽象类派生出来的子类</typeparam>
        /// <param name="ticketType"></param>
        /// <returns></returns>
        public static List<T> GetTickets<T>(string ticketType) where T : Tickets, new()
        {
            List<T> list = new List<T>();
            string sql = @"SELECT * FROM T_TICKETS WHERE TICKETTYPE = @TICKETTYPE";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@TICKETTYPE",ticketType),
            };
            list = SqlHelper.ExecuteReader<T>(sql, sqlParams);
            return list;
        }

        /// <summary>
        /// 反射调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ticType"></param>
        /// <param name="beginning"></param>
        /// <param name="destinaiton"></param>
        /// <returns></returns>
        public static T GetSingleTicketGeneric<T>(string ticType, string beginning, string destinaiton) where T : class, new()
        {
            string sql = "SELECT * FROM T_TICKETS WHERE TICKETTYPE = @TICKETTYPE AND BEGINNING = @BEGINNING AND DESTINATION = @DESTINATION";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@TICKETTYPE",ticType),
                new SqlParameter("@BEGINNING",beginning),
                new SqlParameter("@DESTINATION",destinaiton),
            };

            T res = SqlHelper.ExecuteReaderFirst<T>(sql, sqlParams);
            return res;
        }

        /// <summary>
        /// 反射调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetSingleTicketByIDGeneric<T>(int id) where T : class, new()
        {
            string sql = "SELECT * FROM T_TICKETS WHERE ID = @ID";
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ID",id),
            };

            T res = SqlHelper.ExecuteReaderFirst<T>(sql, sqlParams);
            return res;
        }
    }
}

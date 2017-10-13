using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCommandDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            NonQueryDemo();

            DataReaderDemo();

            AdapterTableDemo();

            SqlParamsDemo();

            ProceduteDemo();
        }

        /// <summary>
        /// 增删改示例
        /// </summary>
        static void NonQueryDemo()
        {
            string sql = "INSERT INTO ACCOUNT (AGE,NAME) VALUES (123,'小明')";
            PrepareCommand(sql, "nonquery");
        }

        /// <summary>
        /// 数据读取器示例
        /// </summary>
        static void DataReaderDemo()
        {
            PrepareCommand("SELECT * FROM ACCOUNT", "datareader");
        }

        /// <summary>
        /// 适配器填充数据表示例
        /// </summary>
        static void AdapterTableDemo()
        {
            PrepareCommand("SELECT * FROM ACCOUNT", "adapter");
        }

        /// <summary>
        /// 准备查询
        /// </summary>
        static void PrepareCommand(string strSql, string execType, CommandType cmdType = CommandType.Text)
        {
            string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";

            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = cmdType;
                cmd.CommandText = strSql;
                Console.WriteLine("====================================================");
                if (execType.ToLower() == "nonquery")
                {
                    //增、删、改 insert、delete、update
                    int res = cmd.ExecuteNonQuery();
                    Console.WriteLine(res);
                }
                else if (execType.ToLower() == "datareader")
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    int fieldCnt = reader.FieldCount;
                    while (reader.Read())
                    {
                        for (int i = 0; i < fieldCnt; i++)
                        {
                            //按照索引访问
                            Console.Write(reader[i] + "\t");

                            //按照列名进行访问
                            //Console.Write(reader["id"]);
                        }
                        Console.WriteLine();
                    }
                }
                else if (execType == "adapter")
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        Console.WriteLine(string.Format("{0}\t{1}\t{2}\t", row["id"], row["age"], row["name"]));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 模拟SQL注入的方式
        /// </summary>
        static void SqlParamsDemo()
        {

            string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";

            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                string uid = "admin";
                string pwd = "admin";

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(1) FROM t_users WHERE USERID = @USERID AND PWD = @PWD ";
                //cmd.Parameters.AddWithValue("@USERID", uid);
                //cmd.Parameters.AddWithValue("@PWD", pwd);

                SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@USERID", uid),
                    new SqlParameter("@PWD", pwd)
                };
                cmd.Parameters.AddRange(sqlParams);

                int res = int.Parse(cmd.ExecuteScalar().ToString());

                if (res > 0)
                {
                    Console.WriteLine("登录成功");
                }
                else
                {
                    Console.WriteLine("不存在该用户");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 存储调用的示例
        /// </summary>
        static void ProceduteDemo()
        {
            string connStr = "server=.;database=TEST_DB;uid=sa;pwd=1;";

            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_GET_AGE";

                SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@NAME","张三"),
                    //output输出参数需要设置属性Direction和Size大小
                    new SqlParameter("@RES","") { Direction = ParameterDirection.Output, Size = 20 }
                };
                cmd.Parameters.AddRange(sqlParams);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                //获取OUTPUT参数返回值
                string res = cmd.Parameters["@RES"].ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}

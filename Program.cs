using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Container.DBTester
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Please run again with the connection string supplied and the table you wish the query. \n Example\tContainer.DBTester.exe 'my stringy connection string of stringliness' 'My Table of tables'"); //Check for connection string and table
            }
            else
            {
                            try 
            { 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.ConnectionString=args[0];
         
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    List<string> queryResp = new List<string>();
                    Console.WriteLine($"Container SQL Query Tester \t{connection.DataSource} \n");
                    Console.Write($"Attempting connection\n");
                    DateTime startTime = DateTime.Now;
                    connection.Open();
                    if (connection.State.ToString() == "Open")
                    {
                        Console.WriteLine($"\tConnection established");
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT TOP (100) [Name]");
                    sb.Append($"FROM {args[1]}");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Console.WriteLine("{0}", reader.GetString(0));
                                queryResp.Add(reader.GetString(0));
                            }
                        }
                    } 
                        DateTime endTime = DateTime.Now;
                        TimeSpan interval = endTime - startTime;
                        Console.WriteLine($"\tQuery Completed");
                        Console.WriteLine("\nQuery Statistics");
                        Console.WriteLine($"\t{queryResp.Count} rows\n\t{interval.Milliseconds}ms from {args[1]}");                 
                }
            }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }  

        }
    }
}

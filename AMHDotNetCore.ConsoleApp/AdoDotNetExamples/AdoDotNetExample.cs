using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMHDotNetCore.ConsoleApp.AdoDotNetExamples
{
    internal class AdoDotNetExample
    {
        public void Run()
        {
            Read();
            //Create();
            //Edit();
            //Delete();
            //Update();
        }

        //Read ............................................................

        private void Edit(int blog_id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AungMinHeinDotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"SELECT [Blog_Id]
                  ,[Blog_Title]
                  ,[Blog_Author]
                  ,[Blog_Content]
                  FROM [dbo].[Tbl_Blog] WHERE Blog_Id = @Blog_ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", blog_id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found!");
                return;
            }
        }

        //Edit ............................................................
        private void Read()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AungMinHeinDotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Opened");

            string query = @"SELECT [Blog_Id]
                  ,[Blog_Title]
                  ,[Blog_Author]
                  ,[Blog_Content]
                  FROM [dbo].[Tbl_Blog]";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            Console.WriteLine("Connection Closed...");

            //data set

            //data table

            //data row

            //data column

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Id => " + dr["Blog_Id"]);
                Console.WriteLine("Title => " + dr["Blog_Title"]);
                Console.WriteLine("Author => " + dr["Blog_Author"]);
                Console.WriteLine("Content => " + dr["Blog_Content"]);
                Console.WriteLine("========================================");
            }
        }

        //Create ..........................................................
        private void Create(string title, string author, string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AungMinHeinDotNetCore",
                UserID = "Sa",
                Password = "sa@123"
            };

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                    ([Blog_Title]
                    ,[Blog_Author]
                    ,[Blog_Content])
                    VALUES
                    (@Blog_Title, @Blog_Author, @Blog_Content)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_Content", content);
            int result = command.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Successfully Saved" : "Saving Failed!";
            Console.WriteLine(message);
        }


        //Delete ...........................................................
        private void Delete(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AungMinHeinDotNetCore",
                UserID = "Sa",
                Password = "sa@123"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                WHERE Blog_Id = @Blog_Id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            int result = command.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Deletion Successful!" : "Deletion Failed";
            Console.WriteLine(message);
        }

        //Update ...........................................................
        private void Update(int id, string title, string author, string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AungMinHeinDotNetCore",
                UserID = "Sa",
                Password = "sa@123"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [Blog_Title] = @Blog_Title
                  ,[Blog_Author] = @Blog_Author
                  ,[Blog_Content] = @Blog_Content
             WHERE Blog_ID = @Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_Content", content);
            int result =command.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            Console.WriteLine(message);
        }
    }
}
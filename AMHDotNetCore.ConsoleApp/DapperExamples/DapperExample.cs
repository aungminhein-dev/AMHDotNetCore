using AMHDotNetCore.ConsoleApp.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMHDotNetCore.ConsoleApp.DapperExamples
{
    public  class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "AungMinHeinDotNetCore",
            UserID = "sa",
            Password = "sa@123"
        };
        public void Run()
        {

            Delete(3);

        }

        private void Read()
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            
                string query = @"SELECT [Blog_Id]
                  ,[Blog_Title]
                  ,[Blog_Author]
                  ,[Blog_Content]
                  FROM [dbo].[Tbl_Blog]";

                List<BlogDataModel> list = db.Query<BlogDataModel>(query).ToList();
                if(list.Count > 0)
                {
                    foreach (BlogDataModel item in list)
                    {
                        Console.WriteLine(item.Blog_Title);
                        Console.WriteLine(item.Blog_Author);
                        Console.WriteLine(item.Blog_Content);
                    }
                }
                else
                {
                    Console.WriteLine("No Data Found!");
                }
            
        }

        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
                string query = @"SELECT [Blog_Id]
                  ,[Blog_Title]
                  ,[Blog_Author]
                  ,[Blog_Content]
                  FROM [dbo].[Tbl_Blog] WHERE Blog_Id = @Blog_Id";
                BlogDataModel? data = db.Query<BlogDataModel>(query,new BlogDataModel { Blog_Id = id}).FirstOrDefault();
                if(data is null)
                {
                    Console.WriteLine("No data found!");
                }
        }

        private void Create(string title, string author, string content)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                    ([Blog_Title]
                    ,[Blog_Author]
                    ,[Blog_Content])
                    VALUES
                    (@Blog_Title, @Blog_Author, @Blog_Content)";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Successfully Saved" : "Saving Failed!";
            Console.WriteLine(message);
        }

        private void Update(int id,string title,string author,string content)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [Blog_Title] = @Blog_Title
                  ,[Blog_Author] = @Blog_Author
                  ,[Blog_Content] = @Blog_Content
               WHERE Blog_Id = @Blog_Id";
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Updated Successfully" : "Updating Failed";
            Console.WriteLine(message); 
        }

        private void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                WHERE Blog_Id = @Blog_Id";
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id
            };
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query,blog);
            string message = result > 0 ? "Deleted" : "Failed";
            Console.WriteLine(message);
        }
    }
}

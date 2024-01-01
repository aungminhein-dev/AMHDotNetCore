using AMHDotNetCore.RestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace AMHDotNetCore.RestApi.EFCoreExamples
{
    public  class EFCoreExample
    {
        private readonly AppDbContext _dbContext = new AppDbContext();

        public void Run()
        {
            Read();
        }

        public void Read()
        {
            var list = _dbContext.Blogs.AsNoTracking().ToList();
            foreach (BlogDataModel item in list)
            {
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        public void Edit(int id)
        {
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(item == null)
            {
                Console.WriteLine("No Data Found!");
            }else
            {
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }     
        }

        public void Create(string title,string author,string content)
        {
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }


        public void Update(int id,string title,string author,string content)
        {
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(item is null)
            {
                Console.WriteLine("No data is found!");
                return;
            }
            else
            {
                item.Blog_Title = title;
                item.Blog_Author = author;  
                item.Blog_Content = content;
            }
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            Console.WriteLine(message);

        }

        private void Delete(int id)
        {

            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                Console.WriteLine("No data is found!");
                return;
            }
            else
            {
                _dbContext.Blogs.Remove(item);
            }
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            Console.WriteLine(message);
        }
    }
}

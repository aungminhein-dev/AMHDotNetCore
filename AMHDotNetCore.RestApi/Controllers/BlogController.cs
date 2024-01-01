using AMHDotNetCore.RestApi.EFCoreExamples;
using AMHDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AMHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _dbContext = new AppDbContext();


        [HttpGet]
        public IActionResult GetBlogs()
        {
            var list = _dbContext.Blogs.ToList();
            return Ok(list);
        }


        [HttpGet(("{id}"))]
        public IActionResult GetBlog(int id) {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(item == null)
            {
                return NotFound("No Data Found!");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            _dbContext.Blogs.Add(blog);
            var result = _dbContext.SaveChanges();
            var message = result > 0 ? "Saving Successful!" : "Saving Failed!";
            return Ok(message);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,BlogDataModel blog)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(item == null) {
                return NotFound("No Data Found!");
            }

            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id ,BlogDataModel blog)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                return NotFound("No Data Found!");
            }

            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }

            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                return NotFound("No Data Found!");
            }
            _dbContext.Remove(item);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }


    }
}

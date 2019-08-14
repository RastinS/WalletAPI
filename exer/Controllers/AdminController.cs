using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuisinessLogic.UsersServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Main.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using DomainLayer.Post;

namespace Main.Controllers
{
    public class AdminController : Controller
    {
        private IAdminUserService adminUserService;
        private DomainLayer.DataManager.WalletContext Context;
        public AdminController(IAdminUserService _adminUserService, DomainLayer.DataManager.WalletContext _Context)
        {
            adminUserService = _adminUserService;
            Context = _Context;
        } 

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.userNum = adminUserService.getUserNum();
            return View();
        }


        #region AddPost

        [Authorize]
        [HttpGet]
        public IActionResult NewPost()
        {
            ViewBag.postTypesList = new List<SelectListItem>
            {
                new SelectListItem {Text = "خبر", Value = "news"},
                new SelectListItem {Text = "مقاله", Value = "article"}
            };
            
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult NewPost(NewPostViewModel model, IFormFile postImage)
        {
            string imageName = postImage.FileName;

            Post newPost = new Post
            {
                Name = model.Name,
                Text = model.Context,
                CreationDate = DateTime.Now,
                PostType = model.Type
            };

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PostImages", imageName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                postImage.CopyToAsync(stream);
            }
            
            return View();
        }

        #endregion

        #region PostGroup
        
        [Authorize]
        [HttpGet]
        public IActionResult AddPostGroup()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPostGroup(AddPostGroupViewModel model)
        {
            PostGroup newPG = new PostGroup
            {
                GroupName = model.Name
            };
            if(!string.IsNullOrEmpty(model.Parent))
            {
                newPG.Parent = Context.PostGroups.Where(p => p.GroupName.Equals(model.Parent)).First();
            }
            else
            {
                newPG.Parent = null;
            }
            Context.PostGroups.Add(newPG);
            if(Context.SaveChanges() == 1)
            {
                ViewBag.message = "با موفقیت اضافه شد";
                return View();
            }
            else
            {
                ViewBag.message = "عملیات موفقیت آمیز نبود، دوباره تلاش کنید";
                return View();
            }
            
        }

        public JsonResult GetPostGroups(string text)
        {
            var groups = Context.PostGroups.Select(g => new { name = g.GroupName });
            if (!string.IsNullOrEmpty(text))
            {
                groups = groups.Where(a => a.name.Contains(text));
            }
            return Json(groups.ToList());
        }
        #endregion

        #region PostTags

        [Authorize]
        [HttpGet]
        public IActionResult AddPostTags()
        {
            return View();
        }

        #endregion
    }
}
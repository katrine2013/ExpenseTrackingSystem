using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseTrackingSystem.Web.Logic;
using ExpenseTrackingSystem.Web.Models;

namespace ExpenseTrackingSystem.Web.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /Tag/
        // select User tags
        [Authorize]
        public ActionResult AllMyTags()
        {
            return View(new TagGeneralFunction().AllUserTags(User.Identity.Name));
        }

        /// <summary>
        /// Edit exists tag
        /// </summary>
        /// <param name="Id">Record Id</param>
        /// <param name="Name">Tag name</param>
        /// <returns>Result message</returns>
        [Authorize]
        [HttpPost]
        public Enums.Messages EditTag(int Id, string Name)
        {
            return new TagGeneralFunction().EditTag(Id, Name, User.Identity.Name);
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="tag">Data model</param>
        /// <returns>Result message</returns>
        [Authorize]
        [HttpPost]
        public ActionResult NewTag(TagModels.CreateTagModel tag)
        {
            if (ModelState.IsValid)
            {
                new TagGeneralFunction().AddTag(tag.Name, User.Identity.Name);
            }

            return RedirectToAction("AllMyTags", "Tag");
        }

        /// <summary>
        /// Delete exists tag
        /// </summary>
        /// <param name="Id">record Id</param>
        /// <returns> result message</returns>
        [Authorize]
        [HttpPost]
        public Enums.Messages DeleteTag(int Id)
        {
            return new TagGeneralFunction().DeleteTag(Id, User.Identity.Name);
        }

    }
}

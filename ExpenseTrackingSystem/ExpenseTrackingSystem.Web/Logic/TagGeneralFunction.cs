using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseTrackingSystem.Database;
using ExpenseTrackingSystem.Database.Expense;
using ExpenseTrackingSystem.Web.Models;

namespace ExpenseTrackingSystem.Web.Logic
{
    public class TagGeneralFunction
    {
        /// <summary>
        /// Search all user's tags or genelar tags
        /// </summary>
        /// <param name="userIdentity">user's identtety info</param>
        /// <returns>tag list</returns>
        public List<TagModels.CreateTagModel> AllUserTags (string userIdentity)
        {
            List<TagModels.CreateTagModel> resultCollection = new List<TagModels.CreateTagModel>();

            using (var context = new ExpenseDatabaseContext())
            {
                var sqlResult = context.Tags.Where(x => !x.IsDelete && (x.User.Login == userIdentity || x.User == null));
                foreach (var tag in sqlResult)
                {
                    resultCollection.Add(new TagModels.CreateTagModel()
                                             {
                                                 Id = tag.TagId,
                                                 Name = tag.Name
                                             });
                }
            }

            return resultCollection;
        }
        
        /// <summary>
        /// Edit tag
        /// </summary>
        /// <param name="id">id tag</param>
        /// <param name="tagName">New name</param>
        /// <returns>Result message</returns>
        public Enums.Messages EditTag(int id, string tagName, string userIdentity)
        {
            using (var context = new ExpenseDatabaseContext())
            {
                //find tag. You can edit just yours tags
                var tag = context.Tags.FirstOrDefault(x => !x.IsDelete && x.TagId == id && x.User.Login == userIdentity);
                if (tag != null)
                {
                    tag.Name = tagName;

                    return context.SaveChanges() == 1 ? Enums.Messages.Good : Enums.Messages.ErorWriteInDatabase;
                }
                else
                {
                    return Enums.Messages.RecordNotFound;
                }

            }
        }

        /// <summary>
        /// Add new tag
        /// </summary>
        /// <param name="tagName">Name of the tag</param>
        /// <param name="userIdentity">Owner</param>
        /// <returns>Result message</returns>
        public Enums.Messages AddTag(string tagName, string userIdentity)
        {
            using (var context = new ExpenseDatabaseContext())
            {
                //find tag. You can edit just yours tags
                var tag = context.Tags.FirstOrDefault(x => x.Name == tagName && !x.IsDelete);
                var user = context.Users.FirstOrDefault(x => x.Login == userIdentity);
                if(user == null)
                {
                    return Enums.Messages.UserNotRegistrate;
                }
                // new tag will add
                if (tag == null)
                {
                    context.Tags.Add(new Tag()
                                         {
                                             Name = tagName,
                                             User = user
                                         });

                    return context.SaveChanges() >= 1 ? Enums.Messages.Good : Enums.Messages.ErorWriteInDatabase;
                }
                else
                {
                    // record exist in user list
                    if (tag.User == user || tag.User == null)
                    {
                        return Enums.Messages.Good;
                    }
                        //add new tag
                    else
                    {
                        // exist in other userTagList
                        context.Tags.Add(new Tag()
                        {
                            Name = tagName,
                            User = user
                        });

                        return context.SaveChanges() >= 1 ? Enums.Messages.Good : Enums.Messages.ErorWriteInDatabase;
                    }
                }

            }
        }

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="id">Record id</param>
        /// <param name="userIdentity">User</param>
        /// <returns>Result message</returns>
        public Enums.Messages DeleteTag(int id, string userIdentity)
        {
            using (var context = new ExpenseDatabaseContext())
            {
                //find tag. You can edit just yours tags.
                var tag = context.Tags.FirstOrDefault(x => !x.IsDelete && x.TagId == id);
                var user = context.Users.FirstOrDefault(x => x.Login == userIdentity);
                if (user == null)
                {
                    return Enums.Messages.UserNotRegistrate;
                }
                if(tag != null && tag.User == user)
                {
                    tag.IsDelete = true;

                    return context.SaveChanges() >= 1 ? Enums.Messages.Good : Enums.Messages.ErorWriteInDatabase;
                }

                return Enums.Messages.RecordNotFound;
            }
        }
    }
}
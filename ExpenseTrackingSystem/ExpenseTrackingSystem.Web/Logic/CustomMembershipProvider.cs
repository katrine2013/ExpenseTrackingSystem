using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using ExpenseTrackingSystem.Database;
using ExpenseTrackingSystem.Database.Security;
using ExpenseTrackingSystem.Web.Helper;

namespace ExpenseTrackingSystem.Web.Logic
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            using (var context = new ExpenseDatabaseContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Login == username);

                string passwordSHA = UserHelper.hashSHA512(password);

                if (user == null)
                {
                    // ExpenseTrackingSystem.Database.Security.User
                    User userRecord = new User();
                    userRecord.UserGuid = Guid.NewGuid().ToString();
                    userRecord.Login = username;
                    userRecord.Password = passwordSHA;
                    context.Users.Add(userRecord);
                    try
                    {
                        if (context.SaveChanges() != 1)
                        {
                            status = MembershipCreateStatus.ProviderError;
                            MvcApplication.log.Error(" Saving were not completed ");

                            return null;  
                        }
                    }
                    catch (UpdateException ex)
                    {
                        status = MembershipCreateStatus.ProviderError;
                        MvcApplication.log.ErrorException(" Exeption during save data in the database: " + ex.StackTrace, ex);

                        return null;
                    }
                }
                else
                {
                    status = MembershipCreateStatus.DuplicateUserName; 

                    return null;
                }

                status = MembershipCreateStatus.Success;

                return null;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            string passwordSHA = UserHelper.hashSHA512(password);
            using (var context = new ExpenseDatabaseContext())
            {
                try
                {
                    User user = context.Users
                        .FirstOrDefault(u => ((u.Login == username) && (u.Password == passwordSHA)));

                    return user != null;
                }
                catch (Exception e)
                {
                    // error read database
                    MvcApplication.log.ErrorException("Exeption during reading database: " + e.Message, e);

                    return false;
                }
            }
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
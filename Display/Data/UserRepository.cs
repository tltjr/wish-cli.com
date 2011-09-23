using System;
using System.Web.Security;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Display.Data
{
    public class UserRepository
    {
        private readonly MongoCollection<User> _collection;
        private readonly ConnectionHelper<User> _connectionHelper = new ConnectionHelper<User>();

        public UserRepository()
        {
            var database = MongoDatabase.Create(_connectionHelper.ConnectionString);
            _collection = database.GetCollection<User>("Users");
        }

        public MembershipUser CreateUser(string username, string password, string email)
        {
            var user = new User
                           {
                               UserName = username,
                               Email = email,
                               Password = BCrypt.HashPassword(password, BCrypt.GenerateSalt()),
                               CreatedDate = DateTime.Now,
                               IsActivated = false,
                               IsLockedOut = false,
                               LastLockedOutDate = DateTime.Now,
                               LastLoginDate = DateTime.Now
                           };

            try
            {
                _collection.Insert(user);
            }
            catch (Exception e)
            {
                return null;
            }
            return GetUser(username);
        }

        public string GetUserNameByEmail(string email)
        {
            var query = Query.EQ("Email", email);
            User result = null;
            try
            {
                result = _collection.FindOne(query);
            }
            catch (Exception e) { }
            return result != null ? result.UserName : String.Empty;
        }

        public MembershipUser GetUser(string username)
        {
            var query = Query.EQ("UserName", username);
            User user = null;
            try
            {
                user = _collection.FindOne(query);
            }
            catch (Exception e) { }
            if (null == user) return null;
            return new MembershipUser("CustomMembershipProvider",
                                                      user.UserName,
                                                      user.Id,
                                                      user.Email,
                                                      "",
                                                      user.Comments,
                                                      user.IsActivated,
                                                      user.IsLockedOut,
                                                      user.CreatedDate,
                                                      user.LastLoginDate,
                                                      DateTime.Now,
                                                      DateTime.Now,
                                                      user.LastLockedOutDate);
        }

        public bool ValidateUser(string username, string password)
        {
            var query = Query.EQ("UserName", username);
            User user;
            try
            {
                user = _collection.FindOne(query);
            }
            catch (Exception e)
            {
                return false;
            }
            return null != user && BCrypt.CheckPassword(password, BCrypt.HashPassword(password, BCrypt.GenerateSalt()));
        }
    }

    public class User
    {
        public ObjectId Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActivated { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime LastLockedOutDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string Comments { get; set; }
    }
}
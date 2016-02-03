using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IUserService
    {
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        InsertResponse CreateUser(UserModel user);

        /// <summary>
        /// Get User by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        FindItemReponse<UserModel> FindUserByID(string Id);

        /// <summary>
        /// Find User by email
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        FindItemReponse<UserModel> FindUserByEmail(string Id);

        /// <summary>
        /// Find User by email
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        FindItemReponse<UserModel> FindUserByUserName(string username);

        /// <summary>
        /// Delete user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteUser(string id);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        BaseResponse UpdateUser(UserModel user);

        /// <summary>
        /// Get all User 
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<UserModel> GetUsers();

        /// <summary>
        /// Get All user except me
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        FindAllItemReponse<UserModel> GetUsersExceptMe(string userID);

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserLoginResponse Login(string username, string password);

        /// <summary>
        /// Logout user
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        BaseResponse Logout(string sessionID);

        /// <summary>
        /// Check User in system before create new User
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        FindItemReponse<UserModel> CheckUsername(string username);

        /// <summary>
        /// Check email in system before create new User
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        FindItemReponse<UserModel> CheckEmail(string email);

    }
}

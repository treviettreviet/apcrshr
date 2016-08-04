using AutoMapper;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation.ModelMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Implementation
{
    public class UserService : IUserService
    {
        public static readonly int TIME_OUT_MINUTES = 30;
        public static readonly string DEFAULT_LANGUAGE = "VN";

        public FindItemReponse<UserModel> FindUserByID(string id)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                User user = userRepository.FindByID(id);
                var _user = MapperUtil.CreateMapper().Mapper.Map<User, UserModel>(user);
                return new FindItemReponse<UserModel>
                {
                    Item = _user,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<UserModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public BaseResponse DeleteUser(string id)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                User user = userRepository.FindByID(id);
                if (user != null)
                {
                    IMailingAddressRepository mailingRepository = RepositoryClassFactory.GetInstance().GetMailingAddressRepository();
                    IList<MailingAddress> mailings = mailingRepository.FindByUserID(id);
                    foreach (var m in mailings)
                    {
                        mailingRepository.Delete(m.MailingAddressID);
                    }
                    userRepository.Delete(id);
                }
                else
                {
                    return new BaseResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Format(Resources.Resource.msg_itemNotExist, "User")
                    };
                }
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_delete_success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public BaseResponse UpdateUser(UserModel user)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                User _user = MapperUtil.CreateMapper().Mapper.Map<UserModel, User>(user);
                userRepository.Update(_user);
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_update_success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindAllItemReponse<UserModel> GetUsers()
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                IList<User> users = userRepository.FindAll();
                var _user = users.Select(u => MapperUtil.CreateMapper().Mapper.Map<User, UserModel>(u)).ToList();
                return new FindAllItemReponse<UserModel>
                {
                    Items = _user,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<UserModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public InsertResponse CreateUser(UserModel user)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                User _user = userRepository.FindByUserName(user.UserName);
                if (_user != null)
                {
                    return new InsertResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = string.Format(Resources.Resource.msg_insert_exists, "Email", user.Email)
                    };
                }
                object id = userRepository.Insert(MapperUtil.CreateMapper().Mapper.Map<UserModel, User>(user));
                return new InsertResponse
                {
                    InsertID = id.ToString(),
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_create_success
                };
            }
            catch (Exception ex)
            {
                return new InsertResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public UserLoginResponse Login(string username, string password)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                User _user = userRepository.FindByUserName(username);
                if (_user != null)
                {
                    if (_user.Locked)
                    {
                        return new UserLoginResponse
                        {
                            ErrorCode = (int)ErrorCode.Error,
                            Message = Resources.Resource.msg_account_locked
                        };
                    }
                }

                IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
                User user = userRepository.Login(username, password);

                if (user != null)
                {
                    try
                    {
                        userSessionRepository.DeleteByUserID(user.UserID);
                    }
                    catch (Exception)
                    {
                    }

                    int timeOut = TIME_OUT_MINUTES * 60 * 1000;

                    UserSession userSession = new UserSession
                    {
                        CreatedDate = DateTime.Now,
                        UserID = user.UserID,
                        SessionID = Guid.NewGuid().ToString(),
                        UpdatedDate = DateTime.Now
                    };

                    object sessionID = userSessionRepository.Insert(userSession);

                    return new UserLoginResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty,
                        SessionId = userSession.SessionID,
                        UserId = user.UserID,
                        UserName = user.UserName
                    };
                }
                else
                {
                    return new UserLoginResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = Resources.Resource.msg_login_fail
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserLoginResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public BaseResponse Logout(string sessionID)
        {
            try
            {
                IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
                userSessionRepository.Delete(sessionID);
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindAllItemReponse<UserModel> GetUsersExceptMe(string userID)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                IList<User> users = userRepository.GetUserExceptMe(userID);
                var _user = users.Select(u => MapperUtil.CreateMapper().Mapper.Map<User, UserModel>(u)).ToList();
                return new FindAllItemReponse<UserModel>
                {
                    Items = _user,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<UserModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindItemReponse<UserModel> FindUserByEmail(string email)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                User user = userRepository.FindByEmail(email);
                var _user = MapperUtil.CreateMapper().Mapper.Map<User, UserModel>(user);
                return new FindItemReponse<UserModel>
                {
                    Item = _user,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<UserModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindItemReponse<UserModel> FindUserByUserName(string username)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                User user = userRepository.FindByUserName(username);
                var _user = MapperUtil.CreateMapper().Mapper.Map<User, UserModel>(user);
                return new FindItemReponse<UserModel>
                {
                    Item = _user,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<UserModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindItemReponse<UserModel> CheckUsername(string username)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                User user = userRepository.FindByUserName(username);
                var _user = MapperUtil.CreateMapper().Mapper.Map<User, UserModel>(user);
                return new FindItemReponse<UserModel>
                {
                    Item = _user,
                    ErrorCode = (int)ErrorCode.Redirect,
                    Message = Resources.Resource.msg_username_exists
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<UserModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindItemReponse<UserModel> CheckEmail(string email)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                User user = userRepository.FindByEmail(email);
                var _user = MapperUtil.CreateMapper().Mapper.Map<User, UserModel>(user);
                return new FindItemReponse<UserModel>
                {
                    Item = _user,
                    ErrorCode = (int)ErrorCode.Redirect,
                    Message = Resources.Resource.msg_email_exists
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<UserModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public BaseResponse ChangePassword(string userID, string newPassword)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                userRepository.ChangePassword(userID, newPassword);
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_update_success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}

using AutoMapper;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
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

        public GetUserResponse FindUserByID(string id)
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    User user = userRepository.FindByID(id);
                    var _user = Mapper.Map<User, UserModel>(user);
                    return new GetUserResponse
                    {
                        User = _user,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetUserResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public BaseResponse DeleteUser(string id)
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    userRepository.Delete(id);
                    return new BaseResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = Resources.AdminResource.msg_delete_success
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

        public BaseResponse UpdateUser(UserModel user)
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    User _user = Mapper.Map<UserModel, User>(user);
                    userRepository.Update(_user);
                    return new BaseResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = Resources.AdminResource.msg_update_success
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

        public GetAllUserResponse GetUsers()
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    IList<User> users = userRepository.FindAll();
                    var _user = users.Select(u => Mapper.Map<User, UserModel>(u)).ToList();
                    return new GetAllUserResponse
                    {
                        Users = _user,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllUserResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public BaseResponse CreateUser(UserModel user)
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    userRepository.Insert(Mapper.Map<UserModel, User>(user));
                    return new BaseResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = Resources.AdminResource.msg_create_success
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

        public UserLoginResponse Login(string username, string password)
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                using (IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository())
                {
                    try
                    {
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
                                SessionId = Guid.NewGuid().ToString(),
                                UpdatedDate = DateTime.Now
                            };

                            object sessionID = userSessionRepository.Insert(userSession);

                            return new UserLoginResponse
                            {
                                ErrorCode = (int)ErrorCode.None,
                                Message = string.Empty,
                                SessionId = userSession.SessionId,
                                UserId = user.UserID,
                                UserName = user.LastName
                            };
                        }
                        else
                        {
                            return new UserLoginResponse
                            {
                                ErrorCode = (int)ErrorCode.Error,
                                Message = Resources.AdminResource.msg_login_fail
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
            }
        }

        public BaseResponse Logout(string sessionID)
        {
            using (IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository())
            {
                try
                {
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
        }

        public GetAllUserResponse GetUsersExceptMe(string userID)
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    IList<User> users = userRepository.GetUserExceptMe(userID);
                    var _user = users.Select(u => Mapper.Map<User, UserModel>(u)).ToList();
                    return new GetAllUserResponse
                    {
                        Users = _user,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllUserResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetAllUserResponse GetUsers(int pageSize, int pageIndex)
        {
            using (IUserRepository usersRepositoty = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    var result = usersRepositoty.FindAll(pageSize, pageIndex);
                    var _user = result.Item2.Select(u => Mapper.Map<User, UserModel>(u)).ToList();
                    return new GetAllUserResponse
                    {
                        Count = result.Item1,
                        Users = _user,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception e)
                {
                    return new GetAllUserResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = e.Message
                    };
                }
            }
        }

        public GetUserResponse FindUserByEmail(string email)
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    User user = userRepository.FindByEmail(email);
                    var _user = Mapper.Map<User, UserModel>(user);
                    return new GetUserResponse
                    {
                        User = _user,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetUserResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetUserResponse FindUserByUserName(string username)
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    User user = userRepository.FindByUserName(username);
                    var _user = Mapper.Map<User, UserModel>(user);
                    return new GetUserResponse
                    {
                        User = _user,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetUserResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetUserResponse CheckUsername(string username)
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    User user = userRepository.FindByUserName(username);
                    var _user = Mapper.Map<User, UserModel>(user);
                    if (_user == null)
                    {
                        return new GetUserResponse
                        {
                            ErrorCode = (int)ErrorCode.None,
                            Message = string.Empty
                        };
                    }
                    return new GetUserResponse
                    {
                        User = _user,
                        ErrorCode = (int)ErrorCode.Redirect,
                        Message = Resources.AdminResource.msg_username_exists
                    };
                }
                catch (Exception ex)
                {
                    return new GetUserResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetUserResponse CheckEmail(string email)
        {
            using (IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository())
            {
                try
                {
                    User user = userRepository.FindByEmail(email);
                    var _user = Mapper.Map<User, UserModel>(user);
                    if (_user == null)
                    {
                        return new GetUserResponse
                        {
                            ErrorCode = (int)ErrorCode.None,
                            Message = string.Empty
                        };
                    }
                    return new GetUserResponse
                    {
                        User = _user,
                        ErrorCode = (int)ErrorCode.Redirect,
                        Message = Resources.AdminResource.msg_email_exists
                    };
                }
                catch (Exception ex)
                {
                    return new GetUserResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }
    }
}

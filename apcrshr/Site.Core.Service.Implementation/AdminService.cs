﻿using AutoMapper;
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
using System.Transactions;

namespace Site.Core.Service.Implementation
{
    public class AdminService : IAdminService
    {
        public static readonly int TIME_OUT_MINUTES = 30;

        public FindItemReponse<AdminModel> FindAdminByID(string id)
        {
            try
            {
                IAdminRepository adminRepository = RepositoryClassFactory.GetInstance().GetAdminRepository();
                Admin admin = adminRepository.FindByID(id);
                var _admin = MapperUtil.CreateMapper().Mapper.Map<Admin, AdminModel>(admin);
                return new FindItemReponse<AdminModel>
                {
                    Item = _admin,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<AdminModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public BaseResponse DeleteAdmin(string id)
        {
            try
            {
                IAdminRepository adminRepository = RepositoryClassFactory.GetInstance().GetAdminRepository();
                adminRepository.Delete(id);
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

        public BaseResponse UpdateAdmin(AdminModel admin)
        {
            try
            {
                IAdminRepository adminRepository = RepositoryClassFactory.GetInstance().GetAdminRepository();
                Admin _admin = MapperUtil.CreateMapper().Mapper.Map<AdminModel, Admin>(admin);
                adminRepository.Update(_admin);
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

        public FindAllItemReponse<AdminModel> GetAdmins()
        {
            try
            {
                IAdminRepository adminRepository = RepositoryClassFactory.GetInstance().GetAdminRepository();
                IList<Admin> admins = adminRepository.FindAll();
                var _admin = admins.Select(n => MapperUtil.CreateMapper().Mapper.Map<Admin, AdminModel>(n)).ToList();
                return new FindAllItemReponse<AdminModel>
                {
                    Items = _admin,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<AdminModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public InsertResponse CreateAdmin(AdminModel admin)
        {
            try
            {
                IAdminRepository adminRepository = RepositoryClassFactory.GetInstance().GetAdminRepository();
                Admin _admin = adminRepository.FindByUserName(admin.UserName);
                if (_admin != null)
                {
                    return new InsertResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = Resources.Resource.msg_username_exists
                    };
                }
                object id = adminRepository.Insert(MapperUtil.CreateMapper().Mapper.Map<AdminModel, Admin>(admin));
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

        public AdminLoginResponse Login(string username, string password)
        {
            try
            {
                IAdminRepository adminRepository = RepositoryClassFactory.GetInstance().GetAdminRepository();
                Admin _admin = adminRepository.FindByUserName(username);
                if (_admin != null)
                {
                    if (_admin.Locked)
                    {
                        return new AdminLoginResponse
                        {
                            ErrorCode = (int)ErrorCode.Error,
                            Message = Resources.Resource.msg_account_locked
                        };
                    }
                }
                Admin admin = adminRepository.Login(username, password);

                if (admin != null)
                {
                    IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();

                    //Delete session id existed
                    try
                    {
                        userSessionRepository.DeleteByUserID(admin.AdminID);
                    }
                    catch (Exception)
                    {
                    }

                    int timeOut = TIME_OUT_MINUTES * 60 * 1000;

                    UserSession userSession = new UserSession
                    {
                        CreatedDate = DateTime.Now,
                        UserID = admin.AdminID,
                        SessionID = Guid.NewGuid().ToString(),
                        UpdatedDate = DateTime.Now
                    };

                    object sessionID = userSessionRepository.Insert(userSession);

                    return new AdminLoginResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = "",
                        SessionId = userSession.SessionID,
                        AdminId = admin.AdminID,
                        AdminName = admin.LastName
                    };
                }
                else
                {
                    return new AdminLoginResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = Resources.Resource.msg_login_fail
                    };
                }
            }
            catch (Exception ex)
            {
                return new AdminLoginResponse
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
                //Delete session id existed
                userSessionRepository.Delete(sessionID);
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.None,
                    Message = ""
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

        public FindAllItemReponse<AdminModel> GetAdminsExceptMe(string adminID)
        {
            try
            {
                IAdminRepository adminRepository = RepositoryClassFactory.GetInstance().GetAdminRepository();
                IList<Admin> admins = adminRepository.GetAdminsExceptMe(adminID);
                var _admin = admins.Select(n => MapperUtil.CreateMapper().Mapper.Map<Admin, AdminModel>(n)).ToList();
                return new FindAllItemReponse<AdminModel>
                {
                    Items = _admin,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<AdminModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
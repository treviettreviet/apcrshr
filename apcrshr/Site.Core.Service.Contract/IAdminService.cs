﻿using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IAdminService
    {
        /// <summary>
        /// Create admin
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        InsertResponse CreateAdmin(AdminModel admin);

        /// <summary>
        /// Get admin by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<AdminModel> FindAdminByID(string id);

        /// <summary>
        /// Delete admin by ID
        /// </summary>
        /// <param name="id"></param>
        BaseResponse DeleteAdmin(string id);

        /// <summary>
        /// Update admin
        /// </summary>
        /// <param name="admin"></param>
        BaseResponse UpdateAdmin(AdminModel admin);

        /// <summary>
        /// Get all admin
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<AdminModel> GetAdmins();

        /// <summary>
        /// Get all admins except me
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns></returns>
        FindAllItemReponse<AdminModel> GetAdminsExceptMe(string adminID);

        /// <summary>
        /// Admin login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        AdminLoginResponse Login(string username, string password);

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="sessionID">string</param>
        /// <returns>Response</returns>
        BaseResponse Logout(string sessionID);

        /// <summary>
        /// Find all standard admin
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<AdminModel> GetStandardAdmins();

        /// <summary>
        /// Find all roles
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<RoleModel> GetRoles();

        /// <summary>
        /// Find available roles
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<RoleModel> GetAvailableRoles();

        /// <summary>
        /// Get all role that not assigned to the specific admin
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns></returns>
        FindAllItemReponse<RoleModel> GetAvailableRoles(string adminID);

        /// <summary>
        /// Get all assigned role for specific admin
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns></returns>
        FindAllItemReponse<RoleModel> GetAssignedRoles(string adminID);

        /// <summary>
        /// Assign roles for specific admin
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="adminID"></param>
        /// <returns></returns>
        BaseResponse AssignRoles(IList<string> roleIds, string adminID);

        /// <summary>
        /// Remove roles for specific admin
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="adminID"></param>
        /// <returns></returns>
        BaseResponse RemoveRoles(IList<string> roleIds, string adminID);

        /// <summary>
        /// Find all resources
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<ResourceModel> GetResources();
    }
}

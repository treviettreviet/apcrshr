using Site.Core.DataModel.Model;
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
        /// Get admin by user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        FindItemReponse<AdminModel> FindAdminByUsername(string username);

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
        /// Get all resource not assigned to the specific role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        FindAllItemReponse<ResourceModel> GetAvailableResources(string roleID);

        /// <summary>
        /// Get all assigned resources for specific role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        FindAllItemReponse<ResourceModel> GetAssignedResources(string roleID);

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
        /// Assign resources for specific role
        /// </summary>
        /// <param name="resourceIds"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        BaseResponse AssignResources(IList<string> resourceIds, string roleID);

        /// <summary>
        /// Remove resources for specific role
        /// </summary>
        /// <param name="resourceIds"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        BaseResponse RemoveResources(IList<string> resourceIds, string roleID);

        /// <summary>
        /// Find all resources
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<ResourceModel> GetResources();

        /// <summary>
        /// Find authorized resource
        /// </summary>
        /// <param name="adminID"></param>
        /// <param name="resourceURL"></param>
        /// <returns></returns>
        FindItemReponse<ResourceModel> GetAuthorizedResource(string adminID, string resourceURL);
        /// <summary>
        /// Get admin by user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        FindItemReponse<AdminModel> FindAdminByEmail(string email);

        /// <summary>
        /// Create new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        InsertResponse CreateRole(RoleModel role);

        /// <summary>
        /// Delete specific role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        BaseResponse DeleteRole(string roleID);
    }
}

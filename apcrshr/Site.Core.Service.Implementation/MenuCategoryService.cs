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

namespace Site.Core.Service.Implementation
{
    public class MenuCategoryService : IMenuCategoryService
    {
        public DataModel.Response.FindAllItemReponse<DataModel.Model.MenuModel> FindAllMenus()
        {
            try
            {
                IMenuRepository menuRepository = RepositoryClassFactory.GetInstance().GetMenuRepository();
                IList<Menu> menus = menuRepository.FindAll();
                var _menus = menus.Select(n => MapperUtil.CreateMapper().Mapper.Map<Menu, MenuModel>(n)).ToList();
                return new FindAllItemReponse<MenuModel>
                {
                    Items = _menus,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<MenuModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.MenuModel> FindAllMenus(string language)
        {
            try
            {
                IMenuRepository menuRepository = RepositoryClassFactory.GetInstance().GetMenuRepository();
                IList<Menu> menus = menuRepository.FindAll(language);
                var _menus = menus.Select(n => MapperUtil.CreateMapper().Mapper.Map<Menu, MenuModel>(n)).ToList();
                return new FindAllItemReponse<MenuModel>
                {
                    Items = _menus,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<MenuModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public InsertResponse CreateMenu(MenuModel menu)
        {
            try
            {
                IMenuRepository menuRepository = RepositoryClassFactory.GetInstance().GetMenuRepository();
                Menu _menu = null;
                if (string.IsNullOrEmpty(menu.ParentID))
                {
                    _menu = menuRepository.FindParentByTitle(menu.Title, menu.Language);
                }
                else
                {
                    _menu = menuRepository.FindByTitleAndParent(menu.Title, menu.ParentID);
                }
                if (_menu != null)
                {
                    return new InsertResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = string.Format(Resources.Resource.msg_insert_exists, Resources.Resource.text_category_title, menu.Title)
                    };
                }
                object id = menuRepository.Insert(MapperUtil.CreateMapper().Mapper.Map<MenuModel, Menu>(menu));
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


        public FindItemReponse<MenuModel> FindByTitle(string title)
        {
            try
            {
                IMenuRepository menuRepository = RepositoryClassFactory.GetInstance().GetMenuRepository();
                Menu _menu = menuRepository.FindByTitle(title);
                return new FindItemReponse<MenuModel>
                {
                    Item = MapperUtil.CreateMapper().Mapper.Map<Menu, MenuModel>(_menu),
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_create_success
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<MenuModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindItemReponse<MenuModel> FindByActionURL(string actionURL)
        {
            try
            {
                IMenuRepository menuRepository = RepositoryClassFactory.GetInstance().GetMenuRepository();
                Menu _menu = menuRepository.FindByActionURL(actionURL);
                return new FindItemReponse<MenuModel>
                {
                    Item = MapperUtil.CreateMapper().Mapper.Map<Menu, MenuModel>(_menu),
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_create_success
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<MenuModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindItemReponse<MenuModel> FindByID(string id)
        {
            try
            {
                IMenuRepository menuRepository = RepositoryClassFactory.GetInstance().GetMenuRepository();
                Menu _menu = menuRepository.FindByID(id);
                return new FindItemReponse<MenuModel>
                {
                    Item = MapperUtil.CreateMapper().Mapper.Map<Menu, MenuModel>(_menu),
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_create_success
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<MenuModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public BaseResponse UpdateMenu(MenuModel menu)
        {
            try
            {
                IMenuRepository menuRepository = RepositoryClassFactory.GetInstance().GetMenuRepository();
                Menu _menu = MapperUtil.CreateMapper().Mapper.Map<MenuModel, Menu>(menu);
                menuRepository.Update(_menu);
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

        public BaseResponse DeleteMenu(string id)
        {
            try
            {
                IMenuRepository menuRepository = RepositoryClassFactory.GetInstance().GetMenuRepository();
                menuRepository.Delete(id);
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


        public MenuDisplayResponse GetMaxDisplayOrder(string title, string id)
        {
            try
            {
                IMenuRepository menuRepository = RepositoryClassFactory.GetInstance().GetMenuRepository();
                int displayOrder = 0;
                string _title = title;
                var menu = menuRepository.FindByID(id);
                if (menu != null)
                {
                    //Has parent
                    if (menu.Menu2 != null)
                    {
                        _title = menu.Menu2.Title;
                        //find all subs
                        IList<Menu> subs = menuRepository.FindSubMenus(menu.Menu2.MenuID);
                        if (subs != null && subs.Count > 0)
                        {
                            displayOrder = subs.Max(s => s.DisplayOrder) + 1;
                        }
                    }
                    else
                    {
                        //find all subs
                        IList<Menu> subs = menuRepository.FindSubMenus(menu.MenuID);
                        if (subs != null && subs.Count > 0)
                        {
                            displayOrder = subs.Max(s => s.DisplayOrder) + 1;
                        }
                    }
                }
                return new MenuDisplayResponse
                {
                    DisplayOrder = displayOrder,
                    Title = _title,
                    ErrorCode = (int)ErrorCode.None
                };
            }
            catch (Exception ex)
            {
                return new MenuDisplayResponse
                {
                    DisplayOrder = 0,
                    Title = title,
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}

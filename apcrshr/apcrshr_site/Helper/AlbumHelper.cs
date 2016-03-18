using Site.Core.Repository;
using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apcrshr_site.Helper
{
    public class AlbumHelper
    {
        public static string GetAlbumName(string albumID)
        {
            IAlbumRepository albumRepository = RepositoryClassFactory.GetInstance().GetAlbumRepository();
            Album album = albumRepository.FindByID(albumID);
            return album != null ? album.Title : albumID;
        }
    }
}
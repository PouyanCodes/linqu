

using linqu.Core.DTOs.Link;
using linqu.DataLayer.Entities.Links;
using System.Collections.Generic;

namespace linqu.Core.Interfaces
{
    public interface ILinkService
    {
        int AddLink(Link link);
        int AddLink(CreateLinkViewModel link);
        void UpdateLink(Link link);
        void EditLink(EditLinkViewModel role);
        void DeleteLink(Link link);
        void DeleteLink(int linkId);



        Link GetLinkById(int linkId);
        EditLinkViewModel GetLinkDataForEdit(int linkId);
        List<LinkDataViewModel> GetUserLinks(int userId);
        string GetLinkWebAddress(string shortKey);
        int GetLinkCount();


        bool IsLinkExist(int linkId);
        bool IsLinkExist(string shortKey);
        bool CheckLinkOwner(int ownerId, int linkId);
    }
}

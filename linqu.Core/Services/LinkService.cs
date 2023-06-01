

using linqu.Core.DTOs.Link;
using linqu.Core.Interfaces;
using linqu.DataLayer.Context;
using linqu.DataLayer.Entities.Links;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linqu.Core.Services
{
    public class LinkService : ILinkService
    {
        private readonly DataBaseContext _context;

        public LinkService(DataBaseContext context)
        {
            _context = context;
        }

        public int AddLink(Link link)
        {
            _context.Links.Add(link);
            _context.SaveChanges();
            return link.LinkId;
        }

        public int AddLink(CreateLinkViewModel link)
        {
            Link newLink = new Link()
            {
                LinkOwnerId = link.LinkOwnerId,
                LinkTitle = link.LinkTitle,
                LinkAddress = link.LinkAddress,
                Description = link.Description,
                CreateDate = DateTime.Now,
                ShortKey = link.ShortKey
            };

            return AddLink(newLink);
        }

        public void UpdateLink(Link link)
        {
            _context.Links.Update(link);
            _context.SaveChanges();
        }

        public void EditLink(EditLinkViewModel link)
        {
            Link editedRole = _context.Links.Find(link.LinkId);

            editedRole.LinkId = link.LinkId;
            editedRole.LinkOwnerId = link.LinkOwnerId;
            editedRole.LinkTitle = link.LinkTitle;
            editedRole.LinkAddress = link.LinkAddress;
            editedRole.Description = link.Description;
            editedRole.ShortKey = link.ShortKey;

            UpdateLink(editedRole);
        }


        public void DeleteLink(Link link)
        {
            link.IsDeleted = true;
            UpdateLink(link);
        }

        public void DeleteLink(int linkId)
        {
            var link = _context.Links.Find(linkId);
            link.IsDeleted = true;
            UpdateLink(link);
        }

        public Link GetLinkById(int linkId)
        {
            return _context.Links.Find(linkId);
        }

        public EditLinkViewModel GetLinkDataForEdit(int linkId)
        {
            var link = _context.Links.Find(linkId);

            return new EditLinkViewModel()
            {
                LinkId = link.LinkId,
                LinkOwnerId = link.LinkOwnerId,
                LinkTitle = link.LinkTitle,
                LinkAddress = link.LinkAddress,
                Description = link.Description,
                ShortKey = link.ShortKey,
            };
        }

        public List<LinkDataViewModel> GetUserLinks(int userId)
        {
            return _context.Links.Where(l => l.LinkOwnerId == userId)
                .Select(l => new LinkDataViewModel()
                {
                    LinkId = l.LinkId,
                    LinkTitle = l.LinkTitle,
                    LinkAddress = l.LinkAddress,
                    Description = l.Description,
                    ShortKey = l.ShortKey,
                    CreateDate = l.CreateDate
                }).ToList();
        }

        public string GetLinkWebAddress(string shortKey)
        {
            return _context.Links.SingleOrDefault(l => l.ShortKey == shortKey).LinkAddress;
        }

        public int GetLinkCount()
        {
            return _context.Links.Count();
        }

        public bool IsLinkExist(int linkId)
        {
            return _context.Links.Any(l => l.LinkId == linkId);
        }

        public bool IsLinkExist(string shortKey)
        {
            return _context.Links.Any(l => l.ShortKey == shortKey);
        }

        public bool CheckLinkOwner(int ownerId, int linkId)
        {
            return _context.Links.Any(l => l.LinkOwnerId == ownerId && l.LinkId == linkId);
        }
    }
}

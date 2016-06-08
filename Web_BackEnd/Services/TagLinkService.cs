using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.Library;
using ProjectPool.DataEntity;
using ProjectPool.Repository;
using System.Data.Entity;

namespace ProjectPool.Services
{
    public class TagLinkService : GenericRepository<TagLink>
    {
        public TagLinkService(string connectionString) : base(connectionString) 
        {
        }

        public TagLinkService(DbContext context)
            : base(context)
        { }

        public void AddTagLinks(string tagIds, int categoryId, int linkId)
        {
            var tagArray = tagIds.Split(',');

            for (int i = 0; i < tagArray.Length; i++)
            {
                int tagId = Convert.ToInt16(tagArray[i]);
                var tagLink = Load(x => x.CategoryId == categoryId && x.LinkId == linkId && x.TagId == tagId).FirstOrDefault();

                if (tagLink == null)
                {
                    TagLink newTagLink = new TagLink()
                    {
                        CategoryId = categoryId,
                        GUID = Guid.NewGuid().ToString(),
                        LinkId = linkId,
                        TagId = Convert.ToInt16(tagArray[i]),
                        DateCreated = DateTime.Now
                    };
                    Add(newTagLink);
                    SaveChanges();
                }
            }
        }

        public void DeleteLinks(int linkId, int categoryId)
        {
            var tagLink = Load(x => x.LinkId == linkId && x.CategoryId == categoryId).ToList();

            foreach (var link in tagLink)
            {
                Remove(link);
                SaveChanges();
            }
        }

        public void DeleteLinks(int tagLinkId)
        {
            var link = Load(x => x.TagLinkId == tagLinkId).FirstOrDefault();

                Remove(link);
                SaveChanges();
        }
        
        public int[] GetLinkedTags(int linkId, int categoryId)
        {
            return Load(x => x.LinkId == linkId && x.CategoryId == categoryId && x.DateDeleted == null).Select(x => x.TagId).ToArray();
        }
    }
}
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;
using OnlineShopMVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.Inf.Repo
{
    public class TagRepo : ITagRepo
    {
        private readonly Context context;

        public TagRepo(Context context)
        {
            this.context = context;
        }
        public Tag GetTagById(int? id)
        {
            return context.Tags.SingleOrDefault(i => i.Id == id);
        }
        public Tag GetTagByName(string? name)
        {
            return context.Tags.Where(i => i.Name.StartsWith(name)).SingleOrDefault();
        }
        public IQueryable GetAllTags()
        {
            return context.Tags;
        }
        public bool RemoveTag(int? id)
        {
            var tag = GetTagById(id);
            if (tag != null)
            {
                context.Remove(tag);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
        public bool UpdateTag(string? name)
        {
            var tagFound = GetTagByName(name);
            if (tagFound != null)
            {
                tagFound.Name = name;
                context.Update(tagFound);
                context.SaveChanges();
                return true;
            }
            else return false;
        }

        public string AddTag(string name)
        {
            Tag tag= new Tag();
            tag.Name = name;

            context.Add(tag);
            context.SaveChanges();
            return tag.Name;  
        }
    }
}

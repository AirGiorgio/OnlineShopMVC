using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.Inf.Data;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;

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
            return context.Tags.Include(x => x.Products).SingleOrDefault(i => i.Id == id);
        }

        public IQueryable GetAllTags(string? name)
        {
            if (name.IsNullOrEmpty())
            {
                return context.Tags;
            }
            else
            {
                return context.Tags.Where(x => x.Name.StartsWith(name));
            }
        }

        public bool RemoveTag(int? id)
        {
            try
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
            catch (Exception)
            {
                return false;
            }
        }

        public string UpdateTag(int id, string? name)
        {
            try
            {
                var tagFound = GetTagById(id);
                if (tagFound != null)
                {
                    tagFound.Name = name;
                    context.Update(tagFound);
                    context.SaveChanges();
                    return "Uaktualniono tag";
                }
                else return "Nie udało się odnaleźć tego tagu";
            }
            catch (Exception)
            {
                return "Wystąpił błąd połączenia z bazą danych";
            }
        }

        public string AddTag(string name)
        {
            try
            {
                Tag tag = new Tag();
                tag.Name = name;

                context.Add(tag);
                context.SaveChanges();
                return "Udało się dodać tag " + tag.Name;
            }
            catch (Exception)
            {
                return "Wystąpił błąd połączenia z bazą danych";
            }
        }

        public bool IsTagNameTaken(string? name)
        {
            if (context.Tags.Any(x => x.Name == name))
            {
                return true;
            }
            else return false;
        }
    }
}
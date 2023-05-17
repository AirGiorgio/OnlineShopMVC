using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMVC.Domain.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAdress { get; set; }
        public string Telephone { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Order> OrderHistory { get; set; }
        public override string ToString()
        {
            return Name + "," + Surname;
        }
    }
}

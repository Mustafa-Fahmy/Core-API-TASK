using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Category : BaseEntity
    {
        
        public ICollection<Products> Products { get; set;} = new List<Products>();
    }
}

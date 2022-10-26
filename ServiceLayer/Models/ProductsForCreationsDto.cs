using DomainLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLayer.Models
{
  

    public class ProductsForCreationsDto 
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string ImgURL { get; set; }
        public int CategoryID { get; set; }
    }
}

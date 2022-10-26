namespace ServiceLayer.Models
{
    public class ProductsForUpdateDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string ImgURL { get; set; }
        public int CategoryID { get; set; }
    }
}

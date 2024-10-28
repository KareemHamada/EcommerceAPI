namespace Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Descrtiption { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }


        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }

        public int TypedId { get; set; }
        public ProductType ProductType { get; set; }
    }
}

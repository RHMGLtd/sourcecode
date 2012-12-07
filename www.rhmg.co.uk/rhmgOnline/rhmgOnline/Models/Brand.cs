namespace rhmgOnline.Models
{
    public class Brand
    {
        public Brand()
        {
            Links = new BrandLink[] { };
        }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string Introduction { get; set; }
        public string MainBrandLink { get; set; }
        public string ImageName { get; set; }
        public BrandLink[] Links { get; set; }
    }

    public class BrandLink
    {
        public string Label { get; set; }
        public string Link { get; set; }
    }
}
namespace rhmg.StudioDiary.Extensions
{
    public static class EnumExtensions
    {
         public static string ToSpacedString(this Product.ProductType value)
         {
             return value.ToString().ToSpacedString();
         }
    }
}
using System.Drawing;
using coolbunny.common.Extensions;
using Machine.Specifications;

namespace coolbunny.tests.common
{
    public class when_resizing_an_image_taller_than_wide
    {
        static Image result;
        static Image origin;
        Establish context = () => origin = Image.FromFile(@"C:\business websites\coolbunny\coolbunny.tests.common\IMG_0659.JPG");
        Because of = () => result = origin.Resize(100);
        It has_the_correct_height = () => result.Width.ShouldEqual(100);
        It has_the_correct_width = () => result.Height.ShouldEqual(134);
    }
    public class when_resizing_an_image_wider_than_tall
    {
        static Image result;
        static Image origin;
        Establish context = () => origin = Image.FromFile(@"C:\business websites\coolbunny\coolbunny.tests.common\IMG_0655.JPG");
        Because of = () => result = origin.Resize(100);
        It has_the_correct_height = () => result.Width.ShouldEqual(100);
        It has_the_correct_width = () => result.Height.ShouldEqual(75);
    }
}
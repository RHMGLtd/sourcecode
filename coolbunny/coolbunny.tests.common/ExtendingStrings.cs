using coolbunny.common.Extensions;
using Machine.Specifications;

namespace coolbunny.tests.common
{
    public class tidying_venue_names
    {
        It leaves_with_spaces_alone = () => "The Blue Room".Tidy().ShouldEqual("The Blue Room");
        It removes_underscores = () => "The_Blue_Room".Tidy().ShouldEqual("The Blue Room");
        It removes_hyphens = () => "The-Blue-Room".Tidy().ShouldEqual("The Blue Room");
        It removes_commas = () => "The Blue Room, Blackpool".Tidy().ShouldEqual("The Blue Room Blackpool");
        It removes_backslashes = () => "The Blue Room / Blackpool".Tidy().ShouldEqual("The Blue Room  Blackpool");
        It replaces_ampersands = () => "Callighan & Wolski".Tidy().ShouldEqual("Callighan and Wolski");
    }
    public class getting_months
    {
        It gets_april_correctly = () => "April".AsMonth().ShouldEqual(4);
        It gets_apr_correctly = () => "Apr".AsMonth().ShouldEqual(4);

        It gets_january_correctly = () => "january".AsMonth().ShouldEqual(1);
    }
    public class linkifying_text
    {
        It updates_on_rhmg_on_its_own =
            () => "rhmg".makeRHMGalink().ShouldEqual("<a href='http://www.rhmg.co.uk' target='_rhmg'>rhmg</a>");

        It updates_on_rhmg_in_a_string =
            () =>
            "updated by rhmg limited manually".makeRHMGalink().ShouldEqual(
                "updated by <a href='http://www.rhmg.co.uk' target='_rhmg'>rhmg</a> limited manually");
    }
    public class ensuring_http
    {
        It adds_http_to_front_of_string_without_any_already =
            () => "www.rhmg.co.uk".EnsureHttp().ShouldEqual("http://www.rhmg.co.uk");

        It does_not_add_if_already_there =
            () => "http://www.rhmg.co.uk".EnsureHttp().ShouldEqual("http://www.rhmg.co.uk")
           ;

        It copes_with_an_empty_string = () => "".EnsureHttp().ShouldEqual("");
        static string input;
        It copes_with_a_null_value = () => input.EnsureHttp().ShouldEqual("");
    }

    public class auto_link_creation
    {
        It detects_links_with_only_www_dot =
            () => "www.rhmg.co.uk".Linkify().ShouldEqual("<a href='http://www.rhmg.co.uk'>www.rhmg.co.uk</a>");

        It detects_fully_qualified_links = () => "http://www.google.com".Linkify().ShouldEqual("<a href='http://www.google.com' target='_blank'>http://www.google.com</a>");
    }
}
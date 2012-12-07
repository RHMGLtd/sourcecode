using blackpoolgigs.common.Models;
using blackpoolgigs.filedal.Providers;
using coolbunny.common.Helpers;
using coolbunny.tests.common.contexts;
using coolbunny.tests.common.Fakes;
using Machine.Specifications;
using OpenFileSystem.IO;

namespace blackpoolgigs.filedal.tests
{
    public class when_getting_a_list_of_band_names_starting_with_an_a : with_service<BandProvider>
    {
        static PageOfResults<BandName> result;
        Establish context = () => Stub<IFileSystem>()
                                      .Setup(x => x.GetDirectory(Moq.It.IsAny<string>()).Files())
                                      .Returns(new[] { new FakeFile().Create("alamaba 3.json"),
                                      new FakeFile().Create("Andrew.json"),
                                      new FakeFile().Create("bryan adams.json")});

        Because of = () => result = Service.GetBands(new PagingParams(1, 10, "", ""), "a");
        It has_returned_two_results = () => result.TotalNumberOfRecords.ShouldEqual(2);
    }
}
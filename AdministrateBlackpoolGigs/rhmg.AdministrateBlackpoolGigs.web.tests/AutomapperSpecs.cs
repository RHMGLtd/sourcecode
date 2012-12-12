using AutoMapper;
using coolbunny.tests.common.contexts;
using Machine.Specifications;
using rhmg.AdministrateBlackpoolGigs.web.Services;

namespace rhmg.AdministrateBlackpoolGigs.web.tests
{
    public class overall_spec_for_automapper : with_service<AutoMapperConfiguration>
    {
        Establish context = () => Service.Do();
        It has_valid_configuration = () => Mapper.AssertConfigurationIsValid();
    }
}
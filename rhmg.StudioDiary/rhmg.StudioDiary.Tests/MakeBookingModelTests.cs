using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using rhmg.StudioDiary.InternalWeb.ViewModels;

namespace rhmg.StudioDiary.Tests
{
    public class when_no_additional_equipment
    {
        It returns_an_empty_dictionary = () => new MakeBookingModel().ExplodeAdditionalEquipment().ShouldBeEmpty();
    }
    public class when_one_additional_equipment
    {
        static MakeBookingModel input;

        Because of = () => input = new MakeBookingModel
                                       {
                                           NumberRequired = new List<string>
                                                                {
                                                                    "eq1_2"
                                                                }
                                       };

        It returns_a_populated_dictionary = () => input.ExplodeAdditionalEquipment().ShouldNotBeEmpty();
        It has_a_valid_id = () => input.ExplodeAdditionalEquipment().First().Key.ShouldEqual("additionalequipment/1");
        It has_the_correct_number = () => input.ExplodeAdditionalEquipment().First().Value.ShouldEqual(2);
    }
}
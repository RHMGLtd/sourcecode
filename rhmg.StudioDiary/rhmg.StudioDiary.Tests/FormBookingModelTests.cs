using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using rhmg.StudioDiary.InternalWeb.ViewModels;
using rhmg.StudioDiary.Tests.Contexts;

namespace rhmg.StudioDiary.Tests
{
    public class when_no_additional_equipment
    {
        It returns_an_empty_dictionary = () => new StandardFormBookingModel().ExplodeAdditionalEquipment(null).ShouldBeEmpty();
    }
    public class when_one_additional_equipment : with_raven_integration<AdditionalEquipment,AdditionalEquipment>
    {
        static StandardFormBookingModel input;

        Establish context = () =>
                                {
                                    session.Store(new AdditionalEquipment
                                                      {
                                                          Description = "test jobby",
                                                          UnitCost = 2.00
                                                      });
                                    session.SaveChanges();
                                };

        Because of = () => input = new StandardFormBookingModel
                                       {
                                           AdditionalEquipmentAndNumberRequired = new List<string>
                                                                {
                                                                    "eq1_2"
                                                                }
                                       };

        It returns_a_populated_dictionary = () => input.ExplodeAdditionalEquipment(session).ShouldNotBeEmpty();
        It has_a_valid_id = () => input.ExplodeAdditionalEquipment(session).First().Id.ShouldEqual("additionalequipment/1");
        It has_the_correct_number = () => input.ExplodeAdditionalEquipment(session).First().UnitCost.ShouldEqual(2);
    }
}
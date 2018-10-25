using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Traveling.UnitTests
{
    [TestFixture]
    public class TravelPlannerTests
    {
        private TravelPlanner _planner;
        private List<TravelCard> unsortedList;

        [SetUp]
        public void SetUp()
        {
            unsortedList = new List<TravelCard>();
            _planner = new TravelPlanner(unsortedList);
        }

        
        [Test]
        [TestCase("parameter1", "parameter1")]
        [TestCase("parameter2", "parameter2")]
        public void ValidateInput_InputParametersAreEqual_ThrowException(string parameter1, string parameter2)
        {
            Assert.That(() => _planner.ValidateInput(parameter1, parameter2), Throws.Exception);
        }

        [Test]
        [TestCase("parameter1", "parameter2")]
        [TestCase("parameter2", "parameter1")]
        public void ValidateInput_InputParametersIsValid_NewCardAddedToList(string departure, string arrival)
        {
            _planner.ValidateInput(departure, arrival);

            Assert.That(unsortedList.Last().Departure, Is.EqualTo(departure));
            Assert.That(unsortedList.Last().Arrival, Is.EqualTo(arrival));
        }

    }
}

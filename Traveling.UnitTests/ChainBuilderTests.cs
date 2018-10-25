using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Traveling;

namespace Traveling.UnitTests
{
    [TestFixture]
    public class ChainBuilderTests
    {
        private ChainBuilder _build;
        private List<string> emptyList = new List<string>();
        private List<string> listOfStrings = new List<string>();
        private List<TravelCard> unsortedList;

        [SetUp]
        public void SetUp()
        {
            unsortedList = new List<TravelCard>() {
                new TravelCard { Departure = "Мельбурн", Arrival = "Кельн" },
                new TravelCard { Departure = "Москва", Arrival = "Париж" },
                new TravelCard { Departure = "Кельн", Arrival = "Москва" }};
            _build = new ChainBuilder(unsortedList);
        }

        #region UniqueProperty method tests

        [Test]
        public void UniqueProperty_IfInputListOfStringsIsEmpty_ThrowArgumentNullException()
        {
            var build = new ChainBuilder(new List<TravelCard>());

            Assert.That(() => build.UniqueProperty(emptyList, "propertyName"), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("propertyName")]
        [TestCase("arrival")]
        [TestCase("departure")]
        [TestCase("rrival")]
        [TestCase("Depart")]
        public void UniqueProperty_IfInputPropertyNameNotEqualDepartureOrArrival_ThrowException(string propertyName)
        {
            var build = new ChainBuilder(new List<TravelCard>());
            listOfStrings.Add("a");

            Assert.That(() => build.UniqueProperty(listOfStrings, propertyName), Throws.Exception);
        }

        [Test]
        public void UniqueProperty_IfDeparturesUniqueListCountMoreTnanOne_ThrowException()
        {
            unsortedList.Add(new TravelCard { Departure = "Лондон", Arrival = "Кельн" });
            var individualPropertyList = _build.IndividualPropertyList("Departure");

            Assert.That(() => _build.UniqueProperty(listOfStrings, "Departure"), Throws.Exception);
        }

        [Test]
        public void UniqueProperty_IfArrivalsUniqueListCountMoreTnanOne_ThrowException()
        {
            unsortedList.Add(new TravelCard { Departure = "Кельн", Arrival = "Лондон" });
            var individualPropertyList = _build.IndividualPropertyList("Arrival");

            Assert.That(() => _build.UniqueProperty(listOfStrings, "Arrival"), Throws.Exception);
        }

        [Test]
        [TestCase("Departure")]
        [TestCase("Arrival")]
        public void UniqueProperty_IfUniqueListIsEmpty_ResultEqualsNull(string propertyName)
        {
            unsortedList.Add(new TravelCard { Departure = "Париж", Arrival = "Мельбурн" });
            var individualPropertyList = _build.IndividualPropertyList(propertyName);

            var result = _build.UniqueProperty(individualPropertyList, propertyName);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void UniqueProperty_DepartureUniqueListCountOnlyOneString_ReturnThisString()
        {
            var individualPropertyList = _build.IndividualPropertyList("Arrival");

            var result = _build.UniqueProperty(individualPropertyList, "Departure");

            Assert.That(result, Is.EqualTo("Мельбурн"));
        }

        [Test]
        public void UniqueProperty_ArrivalUniqueListCountOnlyOneString_ReturnThisString()
        {
            var individualPropertyList = _build.IndividualPropertyList("Departure");

            var result = _build.UniqueProperty(individualPropertyList, "Arrival");

            Assert.That(result, Is.EqualTo("Париж"));
        }
        #endregion


        #region IndividualPropertyList method test
        [Test]
        [TestCase("propertyName")]
        [TestCase("arrival")]
        [TestCase("departure")]
        [TestCase("rrival")]
        [TestCase("Depart")]
        public void IndividualPropertyList_IfInputPropertyNameNotEqualDepartureOrArrival_ThrowException(string propertyName)
        {
            Assert.That(() => _build.IndividualPropertyList(propertyName), Throws.Exception);
        }
        #endregion


        #region Sorting method test
        [Test]
        public void ClosetChain_AddFirstObjectFromUnsortedListToSortedList_FirstObjectFromUnsortedListAddedToSortedList()
        {
            var result = _build.ClosetChain();

            Assert.That(result.First().Departure, Is.EqualTo("Мельбурн"));
            Assert.That(result.First().Arrival, Is.EqualTo("Кельн"));
        }

        [Test]
        public void ClosetChain_SortingClosetChain_ResultFirstObjectDepartureEqualsResultLastObjectArrival()
        {
            var newUnsortedList = new List<TravelCard>() {
                new TravelCard { Departure = "Мельбурн", Arrival = "Кельн" },
                new TravelCard { Departure = "Москва", Arrival = "Мельбурн" },
                new TravelCard { Departure = "Кельн", Arrival = "Москва" }};
            var newBuild = new ChainBuilder(newUnsortedList);

            var result = newBuild.ClosetChain();

            Assert.That(result.First().Departure, Is.EqualTo(result.Last().Arrival));
        }

        [Test]
        public void FirstDepertureSorting_CardWithUniqueDepartureAddFirstToSortedList_ResultFirstCardDepartureEqualsUniqeDeparture()
        {
            var result = _build.FirstDepertureSorting("Мельбурн");

            Assert.That(result.First().Departure, Is.EqualTo("Мельбурн"));
        }


        [Test]
        public void LastArrivalSorting_CardWithUniqueArrivalAddFirstToSortedListAndReverse_ResultLastCardArrivalEqualsUniqeArrival()
        {
            unsortedList.Add(new TravelCard { Departure = "Москва", Arrival = "Мельбурн" });

            var result = _build.LastArrivalSorting("Париж");

            Assert.That(result.Last().Arrival, Is.EqualTo("Париж"));
        }
        #endregion


        #region AddAndRemoveMethod method test
        [Test]
        public void AddAndRemoveMethod_CardIsNull_ThrowArgumentNullException()
        {
            var card = new TravelCard();

            Assert.That(() => _build.AddAndRemoveMethod(card = null), Throws.ArgumentNullException);
        }

        
        [Test]
        public void AddAndRemoveMethod_CardRemoveFromUnsortedList_UnsortedListDidNotContainsThisCard()
        {
            var card = unsortedList[0];

            _build.AddAndRemoveMethod(card);

            Assert.That(unsortedList.Contains(card), Is.False);
        }
        #endregion
    }
}

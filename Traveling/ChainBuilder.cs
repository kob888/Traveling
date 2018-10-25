using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveling
{
    public class ChainBuilder
    {
        private TravelCard tempCard = new TravelCard();
        private List<TravelCard> sortingTravelCards = new List<TravelCard>();
        private List<TravelCard> _travelCards;
        private List<string> uniqueDepartureList = new List<string>();
        private List<string> uniqueArrivalList = new List<string>();
        private int cardsCount;


        public ChainBuilder(List<TravelCard> travelCards)
        {
            _travelCards = travelCards;
            cardsCount = _travelCards.Count;
        }


        public List<TravelCard> SortingCards()
        {
            // Блок сортировки
            try
            {
                sortingTravelCards = SortMethodSelection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }
            
            return sortingTravelCards;
        }

        /// <summary>
        /// Метод выбирает тип сортировки данных из  _travelCards, и возвращает отсортированную коллекцию
        /// </summary>
        public List<TravelCard> SortMethodSelection()
        {
            var listOfDepartures = IndividualPropertyList("Departure");
            var listOfArrivals = IndividualPropertyList("Arrival");

            var uniqueDeparture = UniqueProperty(listOfArrivals, "Departure");
            var uniqueArrival = UniqueProperty(listOfDepartures, "Arrival");

            if (uniqueDeparture != null)
                sortingTravelCards = FirstDepertureSorting(uniqueDeparture);     // сортировка с начала

            else if (uniqueArrival != null)
                sortingTravelCards = LastArrivalSorting(uniqueArrival);          // сортировка с конца

            else
                sortingTravelCards = ClosetChain();                              // сортировка замкнутой цепи

            return sortingTravelCards;
        }


        /// <summary>
        /// Метод возвращает коллекцию  значений свойств Departure или Arrival
        /// </summary>
        public List<string> IndividualPropertyList(string propertyName)
        {
            var list = new List<string>();
            if (propertyName == "Departure")
            {
                foreach (var card in _travelCards)
                    list.Add(card.Departure);
            }
            else if (propertyName == "Arrival")
            {
                foreach (var card in _travelCards)
                    list.Add(card.Arrival);
            }
            else
                throw new Exception("Неверное имя параметра");
            
            return list;
        }

        #region UniqueProperty
        /// <summary>
        /// Метод возвращает уникальное значение свойств Departure или Arrival.
        /// Если таковых нет, возвращается пустая строка
        /// </summary>
        public string UniqueProperty(List<string> propertyList, string propertyName)
        {
            if (propertyList.Count == 0)
                throw new ArgumentNullException();

            var uniqueList = new List<string>();
            string uniqueProperty = null;

            if (propertyName == "Departure")
            {
                // Поиск уникальных Departure  
                for (int i = 0; i < cardsCount; i++)
                {
                    if (!propertyList.Contains(_travelCards[i].Departure))
                        uniqueList.Add(_travelCards[i].Departure);
                }
            }
            else if (propertyName == "Arrival")
            {
                // Поиск уникальных Arrival  
                for (int i = 0; i < cardsCount; i++)
                {
                    if (!propertyList.Contains(_travelCards[i].Arrival))
                        uniqueList.Add(_travelCards[i].Arrival);
                }
            }
            else
                throw new Exception("Неверное имя параметра");
            
            
            // Цепочка не соберется, если уникальных Arrival больше 1
            if (uniqueList.Count > 1)
                throw new Exception("Из вводных данных нельзя собрать полноценную цепочку.");
            else if(uniqueList.Count == 1)
                return uniqueProperty = uniqueList[0];

            return uniqueProperty;
        }
        #endregion

        #region Sorting Methods
        /// <summary>
        /// Метод сортировки замкнутой коллекции карточек путешествия. 
        /// </summary>
        public List<TravelCard> ClosetChain()
        {
            tempCard = _travelCards.First();
            AddAndRemoveMethod(tempCard);

            for (int i = 0; i < cardsCount - 1; i++)
            {
                tempCard = _travelCards.First(d => d.Departure == sortingTravelCards.Last().Arrival);
                AddAndRemoveMethod(tempCard);
            }

            return sortingTravelCards;
        }

        /// <summary>
        /// Метод сортировки по уникальному значению Departure.
        /// Карточка, содержащая уникальный Departure, становится первой в коллекции.
        /// </summary>
        public List<TravelCard> FirstDepertureSorting(string uniqueDeparture)
        {
            tempCard = _travelCards.Single(c => c.Departure == uniqueDeparture);
            AddAndRemoveMethod(tempCard);

            for (int i = 0; i < cardsCount - 1; i++)
            {
                tempCard = _travelCards.First(d => d.Departure == sortingTravelCards.Last().Arrival);
                AddAndRemoveMethod(tempCard);
            }

            return sortingTravelCards;
        }

        /// <summary>
        /// Метод сортировки по уникальному значению Arrival.
        /// Карточка, содержащая уникальный Arrival, после метода Reverse() становится последней в коллекции.
        /// </summary>
        public List<TravelCard> LastArrivalSorting(string uniqueArrival)
        {
            tempCard = _travelCards.Single(c => c.Arrival == uniqueArrival);
            AddAndRemoveMethod(tempCard);

            for (int i = 0; i < cardsCount - 1; i++)
            {
                tempCard = _travelCards.First(d => d.Arrival == sortingTravelCards[i].Departure);
                AddAndRemoveMethod(tempCard);
            }

            sortingTravelCards.Reverse();
            return sortingTravelCards;
        }
        #endregion


        /// <summary>
        /// Метод добавления полученной карточки в коллекцию sortingTravelCards, и последующего удаления ее из коллекции _travelCard.
        /// </summary>
        public void AddAndRemoveMethod(TravelCard tempCard)
        {
            if (tempCard == null)
                throw new ArgumentNullException();

            sortingTravelCards.Add(tempCard);
            _travelCards.Remove(tempCard);
        }

    }
}

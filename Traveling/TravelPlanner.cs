using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveling
{
    public class TravelPlanner
    {
        private List<TravelCard> _travelCards;
        private List<TravelCard> sortinTravelCards = new List<TravelCard>();
        public int Count => _travelCards.Count;

        public TravelPlanner(List<TravelCard> travelCards = null)
        {
            _travelCards = travelCards ?? new List<TravelCard>();
        }


        public void PlanningNewTrip()
        {
            try
            {
                if (_travelCards.Count == 0)
                    InputTravelCards();
                
                var chainBuild = new ChainBuilder(_travelCards);
                sortinTravelCards = chainBuild.SortingCards();
                
                ViewSortingData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }


        public void InputTravelCards()
        {
            int n = 1;

            while (true)
            {
                
                Console.WriteLine("Карточка путешествия. Для завершения нажмите Enter", n);
                Console.WriteLine();

                Console.WriteLine("Введите пункт отправления:");

                var departure = Console.ReadLine();

                if (string.IsNullOrEmpty(departure) || string.IsNullOrWhiteSpace(departure) || departure == "End")
                    break;
                

                Console.WriteLine("Введите пункт назначения:");

                var arrival = Console.ReadLine();

                if (string.IsNullOrEmpty(arrival) || string.IsNullOrWhiteSpace(arrival) || arrival == "End")
                    break;

                ValidateInput(departure, arrival);

                Console.WriteLine();

                n++;
            }

            if (_travelCards.Count == 0)
                throw new ArgumentNullException();

            ViewInputData();
        }


        public void ValidateInput(string inputDeparture, string inputArrival)
        {
            if (!inputDeparture.Equals(inputArrival))
                _travelCards.Add(new TravelCard() { Departure = inputDeparture, Arrival = inputArrival });
            else
                throw new Exception("Города отправления и прибытия не могут совпадать.");
        }


        public void ViewInputData()
        {
            // Вывод всех введенных карточек в консоли.
            Console.WriteLine("--------INPUT--------");

            foreach (var card in _travelCards)
                Console.WriteLine(card.Departure + "-->" + card.Arrival);

            Console.WriteLine("--------INPUT--------");
        }


        public void ViewSortingData()
        {
            // Вывод отсортированного списка в консоли.
            Console.WriteLine("--------OUTPUT--------");

            foreach (var card in sortinTravelCards)
                Console.WriteLine(card.Departure + "-->" + card.Arrival);

            Console.WriteLine("--------OUTPUT--------");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveling
{
    class Program
    {
        private static List<TravelCard> travelCard;
        
        static void Main(string[] args)
        {
            //travelCard = new List<TravelCard>() {
            //    new TravelCard { Departure = "Мельбурн", Arrival = "Кельн" },
            //    new TravelCard { Departure = "Москва", Arrival = "Париж" },
            //    new TravelCard { Departure = "Кельн", Arrival = "Москва" },
            //    new TravelCard { Departure = "Санкт-Петербург", Arrival = "Мельбурн" },
            //    new TravelCard { Departure = "Париж", Arrival = "Лондон" },
            //    new TravelCard { Departure = "Жуковский", Arrival = "Тбилиси" },
            //    new TravelCard { Departure = "Лондон", Arrival = "Нью Йорк" },
            //    new TravelCard { Departure = "Нью Йорк", Arrival = "Лос Анджелес" },
            //    new TravelCard { Departure = "Сочи", Arrival = "Санкт-Петербург" },
            //    new TravelCard { Departure = "Лос Анджелес", Arrival = "Владивосток" },
            //    new TravelCard { Departure = "Тбилиси", Arrival = "Сочи" },
            //    new TravelCard { Departure = "Сеул", Arrival = "Сан Франциско" },
            //    new TravelCard { Departure = "Новосибирск", Arrival = "Пекин" },
            //    new TravelCard { Departure = "Берлин", Arrival = "Сьон" },
            //    new TravelCard { Departure = "Сьон", Arrival = "Стамбул" },
            //    new TravelCard { Departure = "Турин", Arrival = "Берлин" },
            //    new TravelCard { Departure = "Владивосток", Arrival = "Новосибирск" },
            //    new TravelCard { Departure = "Сан Франциско", Arrival = "Майами" },
            //    new TravelCard { Departure = "Пекин", Arrival = "Сеул" },
            //    new TravelCard { Departure = "Майами", Arrival = "Турин" }
            //};

            
            var newTravel = new TravelPlanner(travelCard);
            newTravel.PlanningNewTrip();
        
            Console.ReadKey();
        }

        
    }
}

﻿using Gremlin.Net.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmsoDB_Gremlin
{
    public class Startup
    {
        private static string hostname = "yourhostname.gremlin.cosmosdb.azure.com";
        private static int port = 443;
        private static string authKey = "yourAuthKey";
        private static string database = "yourGraphDbName";
        private static string collection = "yourGraph";

        static async Task Main(string[] args)
        {
            var cityTravels = new List<AirlineTravel>() {
                new AirlineTravel
                {
                    ArrivalCity = "",
                    ArrivalTime = "",
                    DepartureCity = "",
                    DepartureTime = ""
                },
                 new AirlineTravel
                {
                    ArrivalCity = "",
                    ArrivalTime = "",
                    DepartureCity = "",
                    DepartureTime = ""
                },
                  new AirlineTravel
                {
                    ArrivalCity = "",
                    ArrivalTime = "",
                    DepartureCity = "",
                    DepartureTime = ""
                },
                   new AirlineTravel
                {
                    ArrivalCity = "",
                    ArrivalTime = "",
                    DepartureCity = "",
                    DepartureTime = ""
                },
                    new AirlineTravel
                {
                    ArrivalCity = "",
                    ArrivalTime = "",
                    DepartureCity = "",
                    DepartureTime = ""
                }
            };

            var gremlinServer = new GremlinServer(
                hostname, port,
                enableSsl: true,
                username: "/dbs/" + database + "/colls/" + collection,
                password: authKey);

            var constructqueries = GremlinQuery.ConstructTravelPath(cityTravels);
            await GremlinQuery.ExecuteGraphQueriesAsync(gremlinServer, constructqueries);

            foreach (var query in constructqueries)
            {
                var adjacentCount = GremlinQuery.CalculateCountOfAdjacentCities(query);
                Console.WriteLine();
            }

            Console.WriteLine("Graph constructed. B-)");
            Console.ReadLine();
        }
    }
}

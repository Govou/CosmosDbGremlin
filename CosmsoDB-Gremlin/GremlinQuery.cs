using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmsoDB_Gremlin
{
    public class GremlinQuery
    {
        public GremlinQuery(string description, string statement, string vertex)
        {
            Description = description;
            Statement = statement;
            Vertex = vertex;
        }
        private string labelName;

        public string Description { get; private set; }

        public string Statement { get; private set; }

        private static string Vertex { get; set; }

        private string GetLabelName { get { return labelName; } set { labelName = Vertex; } }

        private static string GetVertexStatement(string departure) => $"g.addV('{Vertex}').property('DepartureCity', '{departure}').";

        private static string GetEdgeStatement(string citySource, string cityTarget) => $"g.V('{citySource}').addE('knows').to(g.V('{cityTarget}'))";


        public static IEnumerable<GremlinQuery> ConstructTravelPath(List<AirlineTravel> cities)
        {
            var queries = new List<GremlinQuery>();

            var vertexCount = cities.Count;
            for (var i = 0; i < vertexCount - 1; ++i)
            {
                queries.Insert(i, new GremlinQuery($"Add {cities[i].DepartureCity}", GetVertexStatement(cities[i].DepartureCity), cities[i].DepartureCity));

                var targetName = cities[i + 1];
                queries.Add(new GremlinQuery($"{cities[i].DepartureCity} flies {cities[i].ArrivalCity}", GetEdgeStatement(cities[i].DepartureCity, cities[i].ArrivalCity), null));
             
            }

            // Before executing any queries, we will reset the graph by dropping it.
            queries.Insert(0, new GremlinQuery("Drop existing Graph", "g.V().drop()", null));
            return queries;
        }

        public static string CalculateCountOfAdjacentCities(GremlinQuery query) => $"g.V({query.GetLabelName}).out().path().count()";


        public static async Task ExecuteGraphQueriesAsync(GremlinServer gremlinServer, IEnumerable<GremlinQuery> queries)
        {
            using (var gremlinClient = new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                foreach (var query in queries)
                {
                    try
                    {
                        Console.Write($"Executing: {query.Description}... ");
                        await gremlinClient.SubmitAsync<dynamic>(query.Statement);
                        Console.WriteLine("ok");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        break;
                    }
                }
            }
        }

    }
}

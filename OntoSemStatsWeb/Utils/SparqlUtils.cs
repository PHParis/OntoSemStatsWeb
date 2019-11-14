using System;
using System.Threading.Tasks;
using VDS.RDF.Query;

namespace OntoSemStatsWeb.Utils
{
    public class SparqlUtils
    {
        public static string GetQuery(string command, params (string, string)[] parameters)
        {
            throw new NotImplementedException();
        }
        public static async Task<SparqlResultSet> Select(string selectQuery)
        {
            var endpoint = new SparqlRemoteEndpoint(new Uri("http://live.dbpedia.org/sparql"), "http://dbpedia.org");
            var results = await Task<SparqlResultSet>.Factory.StartNew(() => endpoint.QueryWithResultSet(selectQuery));
            return results;
        }
    }
}
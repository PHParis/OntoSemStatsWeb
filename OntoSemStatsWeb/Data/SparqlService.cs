using System;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace OntoSemStatsWeb.Data
{
    public class SparqlService
    {

        public async Task<OntologyResult> GetDataAsync(OntologyResult ontologyResult)
        {
            // var rng = new Random();
            // return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            // {
            //     Date = startDate.AddDays(index),
            //     TemperatureC = rng.Next(-20, 55),
            //     Summary = Summaries[rng.Next(Summaries.Length)]
            // }).ToArray());
            var files = System.IO.Directory.GetFiles("wwwroot/sparql_queries").Take(2);
            var endpoint = new SparqlRemoteEndpoint(new Uri(ontologyResult.SparqlEndpointUri));
            var g = new Graph();
            foreach (var file in files)
            {
                var query = await System.IO.File.ReadAllTextAsync(file);
                var results = await Task<IGraph>.Factory.StartNew(() => endpoint.QueryWithResultGraph(query));
                g.Merge(results);
            }
            Console.WriteLine(g.Triples.Count);
            ontologyResult.Result = "";
            // VDS.RDF.Writing.Tur
            var writer = new CompressingTurtleWriter(TurtleSyntax.W3C);
            ontologyResult.Result = StringWriter.Write(g, writer);
            // foreach (var triple in g.Triples)
            // {
            //     ontologyResult.Result += triple.ToString() + System.Environment.NewLine;
            //     Console.WriteLine(triple);    
            // }
            // var results = await Task<SparqlResultSet>.Factory.StartNew(() => endpoint.QueryWithResultSet(@"
            // PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            // PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            // PREFIX owl:<http://www.w3.org/2002/07/owl#>
            // PREFIX void:<http://rdfs.org/ns/void#>
            // CONSTRUCT 
            // {
            //     _:b0 a void:Dataset ; :hasStat [
            //         a :Stat ; :hasSemanticFeature :OwlAllDisjointClasses ;
            //         :definitionCount ?definitionsCount ]
            // }
            // WHERE
            // {
            //     {
            //         SELECT (COUNT (*) as ?definitionsCount)
            //         WHERE 
            //         {
            //             {
            //                 ?p a owl:AllDisjointClasses .
            //             } 
            //         }
            //     }
            // }"));
            return ontologyResult;
            // throw new NotImplementedException();
        }
    }
}

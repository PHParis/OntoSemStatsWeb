using System;
using System.Collections.Generic;
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
            ontologyResult.Result = new Dictionary<string, Dictionary<string, string>>();
            var endpoint = new SparqlRemoteEndpoint(new Uri(ontologyResult.SparqlEndpointUri));
            var resultsVoid = await Task<SparqlResultSet>.Factory.StartNew(() => endpoint.QueryWithResultSet(@"
            PREFIX void:<http://rdfs.org/ns/void#>
            SELECT ?ds
            WHERE {
                ?ds a void:Dataset . 
            }"));
            var g = new Graph();
            var ds = (INode)g.CreateBlankNode();
            if (!resultsVoid.IsEmpty)
            {
                ds = resultsVoid.First()["ds"];
            }
            g.Assert(ds, g.CreateUriNode(new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#type")), g.CreateUriNode(new Uri("http://rdfs.org/ns/void#Dataset")));
            // var rng = new Random();
            // return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            // {
            //     Date = startDate.AddDays(index),
            //     TemperatureC = rng.Next(-20, 55),
            //     Summary = Summaries[rng.Next(Summaries.Length)]
            // }).ToArray());
            var files = System.IO.Directory.GetFiles("wwwroot/sparql_queries/");//.Take(2);
            // var g = new Graph();
            var baseUri = "http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#";
            foreach (var file in files)
            {
                var query = await System.IO.File.ReadAllTextAsync(file);
                // var results = await Task<IGraph>.Factory.StartNew(() => endpoint.QueryWithResultGraph(query));
                var results = await Task<SparqlResultSet>.Factory.StartNew(() => endpoint.QueryWithResultSet(query));
                // g.Merge(results);
                if (results.IsEmpty) continue;
                var result = results.First();
                var definitionsCount = ((ILiteralNode)result["definitionsCount"]).Value;
                if (definitionsCount == "0") continue;                
                var feature = result["feature"].ToString();
                var lastPart = feature.Split("#").Last();
                var triples = result.Variables.Contains("triples") ? ((ILiteralNode)result["triples"]).Value : "";
                var stat = g.CreateBlankNode();
                g.Assert(ds, g.CreateUriNode(new Uri(baseUri + "hasStat")), stat);
                g.Assert(stat, g.CreateUriNode(new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#type")), g.CreateUriNode(new Uri(baseUri + "Stat")));
                g.Assert(stat, g.CreateUriNode(new Uri(baseUri + "hasSemanticFeature")), g.CreateUriNode(new Uri(feature)));
                g.Assert(stat, g.CreateUriNode(new Uri(baseUri + "definitionCount")), g.CreateLiteralNode(definitionsCount, new Uri("http://www.w3.org/2001/XMLSchema#integer")));
                ontologyResult.Result[lastPart] = new Dictionary<string, string>()
                {
                    {"definitionsCount", definitionsCount}
                };
                if (triples != "0")
                {                    
                    g.Assert(stat, g.CreateUriNode(new Uri(baseUri + "triples")), g.CreateLiteralNode(triples, new Uri("http://www.w3.org/2001/XMLSchema#integer")));
                    ontologyResult.Result[lastPart].Add("triples", triples);
                }
            }
            Console.WriteLine(g.Triples.Count);
           
            // VDS.RDF.Writing.Tur
            // var writer = new VDS.RDF.Writing.HtmlWriter();
            var writer = new CompressingTurtleWriter(TurtleSyntax.W3C);
            ontologyResult.Turtle = StringWriter.Write(g, writer);




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

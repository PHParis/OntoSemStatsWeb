using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OntoSemStatsWeb.Utils;
using OntoSemStatsWeb.Utils.Vocabularies;
using VDS.RDF;
using VDS.RDF.Query;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task FunctionalStatsOnDbpedia()
        {
            var queryString = new SparqlParameterizedString();
            queryString.CommandText = @"
                SELECT DISTINCT ?s WHERE 
                { 
                    ?s @property @value 
                }";
            queryString.SetUri("value", OWL.ClassFunctionalProperty);
            queryString.SetUri("property", RDF.PropertyType);

            var results = await SparqlUtils.Select(queryString.ToString());
            var funcProps = results.Results.Select(r => r["s"]).ToHashSet();
            var otherFunc = new HashSet<INode>();
            foreach (var func in funcProps)
            {
                queryString = new SparqlParameterizedString();
                queryString.CommandText = @"
                    SELECT DISTINCT ?s WHERE 
                    { 
                        ?s (@property)+ @value 
                    }";
                queryString.SetUri("value", new Uri(func.ToString()));
                queryString.SetUri("property", RDFS.PropertySubPropertyOf);
                results = await SparqlUtils.Select(queryString.ToString());
                if (results.Any())
                {
                    var newFuncProps = results.Results.Select(r => r["s"]).ToHashSet();
                    otherFunc.UnionWith(newFuncProps);
                }
            }
            funcProps.UnionWith(otherFunc);

            // foreach (var funcProp in funcProps)
            // {
            //     queryString = new SparqlParameterizedString();
            //     queryString.CommandText = @"
            //         SELECT (COUNT(DISTINCT ?s) as ?subCount) (COUNT(*) as ?tripleCount) WHERE 
            //         { 
            //             ?s @property ?o 
            //         }";
            //     queryString.SetUri("property", new Uri(funcProp.ToString()));
            //     results = await SparqlUtils.Select(queryString.ToString());
            //     Console.WriteLine(results.FirstOrDefault()?.ToString() ?? "None");
            // }
            queryString = new SparqlParameterizedString();
            queryString.CommandText = @"
                SELECT (COUNT(DISTINCT ?s) as ?subCount) (COUNT(*) as ?tripleCount) WHERE 
                { 
                    VALUES ?p { XXX }
                    ?s ?p ?o 
                }".Replace("XXX", String.Join(" ", funcProps.Select(x => $"<{x.ToString()}>")));
            results = await SparqlUtils.Select(queryString.ToString());
            Console.WriteLine(results.FirstOrDefault()?.ToString() ?? "None");
        }
    }
}

using System;
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
        public async Task Test1()
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
                }

            }
        }
    }
}

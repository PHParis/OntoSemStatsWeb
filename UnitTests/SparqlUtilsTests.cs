using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OntoSemStatsWeb.Utils;
using OntoSemStatsWeb.Utils.Vocabularies;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        // TODO: penser à tester si deux Inodes identiques appartenant à deux graphes différents sont considérés comme les mêmes ????
        // TODO: y a t il un moyen plus simple de naviguer entre les propriétés et de charger les données manquantes éventuellement ??? Voir exemple de foaf ci-dessous

        [Fact]
        public async Task LoadFromUri()
        {
            // Télécharger le graphe de Paris
            var g = new Graph();
            try
            {
                g.LoadFromUri(new Uri("http://dbpedia.org/resource/Paris"), new NTriplesParser());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine(g.Triples.Count);
            Console.WriteLine(g.Triples.Select(x => x.Predicate).Distinct().Count());
            foreach (var p in g.Triples.Select(x => x.Predicate).Distinct().OrderBy(x => x.ToString()))
            {
                Console.WriteLine(p);
            }
            Console.WriteLine($"Paris est sujet de certains triplets : {g.GetTriplesWithSubject(new Uri("http://dbpedia.org/resource/Paris")).Any()}");
            Console.WriteLine($"Paris est objet de certains triplets : {g.GetTriplesWithObject(new Uri("http://dbpedia.org/resource/Paris")).Any()}");

            // foaf name n'étant pas défini dans DBpedia comme ceci le prouve
            var foafName = "http://xmlns.com/foaf/0.1/name";
            var queryString = new SparqlParameterizedString();
            queryString.CommandText = @"
                SELECT * WHERE 
                { 
                    @property ?p ?o 
                }";
            queryString.SetUri("property", new Uri(foafName));

            var results = await SparqlUtils.Select(queryString.ToString());
            Console.WriteLine(results.IsEmpty);

            // il faut donc charger la définition de foaf name depuis son URI
            var g2 = new Graph();
            try
            {
                g2.LoadFromUri(new Uri(foafName)); // ne marche pas avec RdfXmlParser
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine(g2.Triples.Count);
        }

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

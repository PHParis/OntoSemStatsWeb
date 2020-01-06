// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using OntoSemStatsLib.ProcessResult;
// using OntoSemStatsLib.Utils;
// using OntoSemStatsLib.Utils.Vocabularies;
// using VDS.RDF;
// using VDS.RDF.Parsing;
// using VDS.RDF.Query;
// using Xunit;

// namespace UnitTests
// {
//     public class UnitTest1
//     {

//         [Fact]
//         public async Task LoadFromUri()
//         {
//             // Télécharger le graphe de Paris
//             var g = new Graph();
//             try
//             {
//                 g.LoadFromUri(new Uri("http://dbpedia.org/resource/Paris"), new NTriplesParser());
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e.ToString());
//             }
//             Console.WriteLine(g.Triples.Count);
//             Console.WriteLine(g.Triples.Select(x => x.Predicate).Distinct().Count());
//             foreach (var p in g.Triples.Select(x => x.Predicate).Distinct().OrderBy(x => x.ToString()))
//             {
//                 Console.WriteLine(p);
//             }
//             Console.WriteLine($"Paris est sujet de certains triplets : {g.GetTriplesWithSubject(new Uri("http://dbpedia.org/resource/Paris")).Any()}");
//             Console.WriteLine($"Paris est objet de certains triplets : {g.GetTriplesWithObject(new Uri("http://dbpedia.org/resource/Paris")).Any()}");

//             // foaf name n'étant pas défini dans DBpedia comme ceci le prouve
//             var foafName = "http://xmlns.com/foaf/0.1/name";
//             var queryString = new SparqlParameterizedString();
//             queryString.CommandText = @"
//                 SELECT * WHERE 
//                 { 
//                     @property ?p ?o 
//                 }";
//             queryString.SetUri("property", new Uri(foafName));

//             var results = await SparqlUtils.Select(queryString.ToString());
//             Console.WriteLine(results.IsEmpty);

//             // il faut donc charger la définition de foaf name depuis son URI
//             var g2 = new Graph();
//             try
//             {
//                 g2.LoadFromUri(new Uri(foafName)); // ne marche pas avec RdfXmlParser
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e.ToString());
//             }
//             Console.WriteLine(g2.Triples.Count);
//         }

//         public static Dictionary<string, int> IncrementDictDictionary(string value, Dictionary<string, int> dict)
//         {
//             if (dict.ContainsKey(value))
//             {
//                 dict[value]++;
//             }
//             else
//             {
//                 dict[value] = 1;
//             }
//             return dict;
//         }

//         public IEnumerable<SparqlResult> YieldTriples()
//         {
//             var offset = 0;
//             var nbRows = 10000;
//             var nbResults = 0;
//             while (true)
//             {
//                 var queryString = new SparqlParameterizedString();
//                 queryString.CommandText = @"
//                 SELECT * WHERE 
//                 { 
//                     ?s ?p ?o 
//                 }
//                 LIMIT 10000                
//                 OFFSET " + offset;
//                 // queryString.SetVariable("offset", 0);
//                 SparqlResultSet results = null;
//                 try
//                 {
//                     results = SparqlUtils.Select(queryString.ToString()).Result;
//                 }
//                 catch (Exception e)
//                 {
//                     Console.WriteLine(e.InnerException);
//                 }
//                 if (results == null || results.IsEmpty)
//                     break;

//                 offset += nbRows;
//                 foreach (var result in results)
//                 {
//                     yield return result;
//                 }
//             }
//         }

//         [Fact]
//         public void Stats()
//         {
//             var stats = new ComplexStat(YieldTriples().Select(x => new BasicStat(x.ToTrpl())).ToList());
//             Console.WriteLine(stats.FunctionalPropertyDefinedCount);
//             Assert.True(stats.FunctionalPropertyDefinedCount == 30);
//         }

//         [Fact]
//         public async Task GetAllUsedPropAndClasses()
//         {
//             var usedPropertyCount = new Dictionary<string, int>();
//             var usedClassCount = new Dictionary<string, int>();

//             var offset = 0;
//             var nbRows = 10000;
//             var nbResults = 0;
//             while (true)
//             {
//                 var queryString = new SparqlParameterizedString();
//                 queryString.CommandText = @"
//                 SELECT * WHERE 
//                 { 
//                     ?s ?p ?o 
//                 }
//                 LIMIT 10000                
//                 OFFSET " + offset;
//                 // queryString.SetVariable("offset", 0);

//                 try
//                 {
//                     var results = await SparqlUtils.Select(queryString.ToString());
//                     results.ProcessSparqlResultSet();
//                     nbResults += results.Count;
//                     if (results.IsEmpty)
//                     {
//                         Console.WriteLine($"{nbResults} results collected!");
//                         break;
//                     }
//                     foreach (var r in results)
//                     {
//                         var p = r["p"].ToString();
//                         IncrementDictDictionary(p, usedPropertyCount);
//                         if (p == RDF.PropertyType.ToString())
//                         {
//                             var o = r["o"].ToString();
//                             IncrementDictDictionary(o, usedClassCount);
//                         }
//                     }
//                 }
//                 catch (Exception e)
//                 {
//                     Console.WriteLine(e.InnerException);
//                 }
//                 offset += nbRows;
//                 if (usedPropertyCount.Count > 9_000_000)
//                 {
//                     break;
//                 }
//             }
//             // Console.WriteLine(string.Join(Environment.NewLine, usedProps));
//             Console.WriteLine(usedPropertyCount.Count);
//         }

//         [Fact]
//         public void PipelineTest()
//         {
//             var t = new Trpl<string>(
//                 "Instance",
//                 RDF.PropertyType.ToString(),
//                 OWL.ClassFunctionalProperty.ToString());

//         }

//         [Fact]
//         public async Task CheckIfResultsAreAlwaysInTheSameOrder()
//         {
//             // oui
//             var rnd = new Random();
//             var queryString = new SparqlParameterizedString();
//             var lists = new List<SparqlResult[]>();
//             foreach (var i in Enumerable.Range(0, 100))
//             {
//                 var limit = rnd.Next(3, 20);
//                 queryString.CommandText = @"
//                 SELECT * WHERE 
//                 { 
//                     ?s ?p ?o 
//                 }
//                 LIMIT " + limit;
//                 var results = await SparqlUtils.Select(queryString.ToString());
//                 lists.Add(results.Take(3).ToArray());
//             }
//             Assert.True(lists.Select(sr => sr.FirstOrDefault()["s"]).Distinct().Count() == 1);
//             Assert.True(lists.Select(sr => sr.FirstOrDefault()["p"]).Distinct().Count() == 1);
//             Assert.True(lists.Select(sr => sr.FirstOrDefault()["o"]).Distinct().Count() == 1);

//             Assert.True(lists.Select(sr => sr.Skip(1).FirstOrDefault()["s"]).Distinct().Count() == 1);
//             Assert.True(lists.Select(sr => sr.Skip(1).FirstOrDefault()["p"]).Distinct().Count() == 1);
//             Assert.True(lists.Select(sr => sr.Skip(1).FirstOrDefault()["o"]).Distinct().Count() == 1);

//             Assert.True(lists.Select(sr => sr.Skip(2).FirstOrDefault()["s"]).Distinct().Count() == 1);
//             Assert.True(lists.Select(sr => sr.Skip(2).FirstOrDefault()["p"]).Distinct().Count() == 1);
//             Assert.True(lists.Select(sr => sr.Skip(2).FirstOrDefault()["o"]).Distinct().Count() == 1);
//         }

//         [Fact]
//         public async Task FunctionalStatsOnDbpedia()
//         {
//             var queryString = new SparqlParameterizedString();
//             queryString.CommandText = @"
//                 SELECT DISTINCT ?s WHERE 
//                 { 
//                     ?s @property @value 
//                 }";
//             queryString.SetUri("value", OWL.ClassFunctionalProperty);
//             queryString.SetUri("property", RDF.PropertyType);

//             var results = await SparqlUtils.Select(queryString.ToString());
//             var funcProps = results.Results.Select(r => r["s"]).ToHashSet();
//             var otherFunc = new HashSet<INode>();
//             foreach (var func in funcProps)
//             {
//                 queryString = new SparqlParameterizedString();
//                 queryString.CommandText = @"
//                     SELECT DISTINCT ?s WHERE 
//                     { 
//                         ?s (@property)+ @value 
//                     }";
//                 queryString.SetUri("value", new Uri(func.ToString()));
//                 queryString.SetUri("property", RDFS.PropertySubPropertyOf);
//                 results = await SparqlUtils.Select(queryString.ToString());
//                 if (results.Any())
//                 {
//                     var newFuncProps = results.Results.Select(r => r["s"]).ToHashSet();
//                     otherFunc.UnionWith(newFuncProps);
//                 }
//             }
//             funcProps.UnionWith(otherFunc);

//             // foreach (var funcProp in funcProps)
//             // {
//             //     queryString = new SparqlParameterizedString();
//             //     queryString.CommandText = @"
//             //         SELECT (COUNT(DISTINCT ?s) as ?subCount) (COUNT(*) as ?tripleCount) WHERE 
//             //         { 
//             //             ?s @property ?o 
//             //         }";
//             //     queryString.SetUri("property", new Uri(funcProp.ToString()));
//             //     results = await SparqlUtils.Select(queryString.ToString());
//             //     Console.WriteLine(results.FirstOrDefault()?.ToString() ?? "None");
//             // }
//             queryString = new SparqlParameterizedString();
//             queryString.CommandText = @"
//                 SELECT (COUNT(DISTINCT ?s) as ?subCount) (COUNT(*) as ?tripleCount) WHERE 
//                 { 
//                     VALUES ?p { XXX }
//                     ?s ?p ?o 
//                 }".Replace("XXX", String.Join(" ", funcProps.Select(x => $"<{x.ToString()}>")));
//             results = await SparqlUtils.Select(queryString.ToString());
//             Console.WriteLine(results.FirstOrDefault()?.ToString() ?? "None");
//         }
//     }
// }

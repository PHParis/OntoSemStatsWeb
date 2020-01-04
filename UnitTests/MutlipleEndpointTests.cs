using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Writing;
using Xunit;

namespace UnitTests
{
    public class MutlipleEndpointTests
    {
        static string[] queries = new[] {
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlAllDifferent }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a owl:AllDifferent .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlAllDisjointClasses }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a owl:AllDisjointClasses .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlSameAs }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:sameAs ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlAllDisjointProperties }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a owl:AllDisjointProperties .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlDatatypeComplementOf }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:datatypeComplementOf ?o .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlDifferentFrom }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:differentFrom ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlDisjointUnionOf }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:disjointUnionOf ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlDisjointWith }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:disjointWith ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :RdfsDomain }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p rdfs:domain ?o .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlEquivalentClass }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:equivalentClass ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlEquivalentProperty }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:equivalentProperty ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlInverseOf }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:inverseOf ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlNegativePropertyAssertion }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a owl:NegativePropertyAssertion .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlPropertyChainAxiom }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:propertyChainAxiom ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            PREFIX void:<http://rdfs.org/ns/void#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlPropertyDisjointWith }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:propertyDisjointWith ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :RdfsRange }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p rdfs:range ?o .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :RdfsSubClassOf }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p rdfs:subClassOf ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :RdfsSubProperty }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p rdfs:subProperty ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :RdfType }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a ?p2 .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?feature
            WHERE
            {
                values ?feature { :OwlWithRestrictions }
                {
                    SELECT (COUNT (*) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p owl:withRestrictions ?o .
                        } 
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?triples ?feature
            WHERE
            {
                values ?feature { :OwlAsymmetricProperty }
                {
                    SELECT (COUNT (DISTINCT ?p) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a owl:AsymmetricProperty .
                        } 
                    }
                }
                {
                    SELECT (COUNT (*) as ?triples)
                    WHERE
                    {
                        {
                            SELECT DISTINCT ?p
                            WHERE 
                            {
                                {
                                    ?p a owl:AsymmetricProperty .
                                } 
                            }
                        }
                        ?s ?p ?o .
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?triples ?feature
            WHERE
            {
                values ?feature { :OwlFunctionalProperty }
                {
                    SELECT (COUNT (DISTINCT ?p) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a owl:FunctionalProperty .
                        } 
                    }
                }
                {
                    SELECT (COUNT (*) as ?triples)
                    WHERE
                    {
                        {
                            SELECT DISTINCT ?p
                            WHERE 
                            {
                                {
                                    ?p a owl:FunctionalProperty .
                                } 
                            }
                        }
                        ?s ?p ?o .
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?triples ?feature
            WHERE
            {
                values ?feature { :OwlInverseFunctionalProperty }
                {
                    SELECT (COUNT (DISTINCT ?p) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a owl:InverseFunctionalProperty .
                        } 
                    }
                }
                {
                    SELECT (COUNT (*) as ?triples)
                    WHERE
                    {
                        {
                            SELECT DISTINCT ?p
                            WHERE 
                            {
                                {
                                    ?p a owl:InverseFunctionalProperty .
                                } 
                            }
                        }
                        ?s ?p ?o .
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?triples ?feature
            WHERE
            {
                values ?feature { :OwlIrreflexiveProperty }
                {
                    SELECT (COUNT (DISTINCT ?p) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a owl:IrreflexiveProperty .
                        } 
                    }
                }
                {
                    SELECT (COUNT (*) as ?triples)
                    WHERE
                    {
                        {
                            SELECT DISTINCT ?p
                            WHERE 
                            {
                                {
                                    ?p a owl:IrreflexiveProperty .
                                } 
                            }
                        }
                        ?s ?p ?o .
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?triples ?feature
            WHERE
            {
                values ?feature { :OwlReflexiveProperty }
                {
                    SELECT (COUNT (DISTINCT ?p) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a owl:ReflexiveProperty .
                        } 
                    }
                }
                {
                    SELECT (COUNT (*) as ?triples)
                    WHERE
                    {
                        {
                            SELECT DISTINCT ?p
                            WHERE 
                            {
                                {
                                    ?p a owl:ReflexiveProperty .
                                } 
                            }
                        }
                        ?s ?p ?o .
                    }
                }
            }",
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#> 
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#> 
            PREFIX owl:<http://www.w3.org/2002/07/owl#> 
            SELECT ?definitionsCount ?triples ?feature 
            WHERE 
            {
                values ?feature { :OwlSymmetricProperty } 
                {
                    SELECT (COUNT (DISTINCT ?p) as ?definitionsCount) 
                    WHERE 
                    {
                        {
                            ?p a owl:SymmetricProperty . 
                        } 
                    }
                }
                {
                    SELECT (COUNT (*) as ?triples) 
                    WHERE 
                    {
                        {
                            SELECT DISTINCT ?p 
                            WHERE 
                            {
                                {
                                    ?p a owl:SymmetricProperty . 
                                } 
                            }
                        }
                        ?s ?p ?o . 
                    }
                }
            }"
            ,
            @"PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
            PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
            PREFIX owl:<http://www.w3.org/2002/07/owl#>
            SELECT ?definitionsCount ?triples ?feature
            WHERE
            {
                values ?feature { :OwlTransitiveProperty }
                {
                    SELECT (COUNT (DISTINCT ?p) as ?definitionsCount)
                    WHERE 
                    {
                        {
                            ?p a owl:TransitiveProperty .
                        } 
                    }
                }
                {
                    SELECT (COUNT (*) as ?triples)
                    WHERE
                    {
                        {
                            SELECT DISTINCT ?p
                            WHERE 
                            {
                                {
                                    ?p a owl:TransitiveProperty .
                                } 
                            }
                        }
                        ?s ?p ?o .
                    }
                }
            }"
        };
        [Fact]
        public void TestName()
        {
            var endpoints = System.IO.File.ReadAllLines(@"C:\dev\dotnet\OntoSemStatsWeb\UnitTests\endpoints").Distinct();
            var g = new Graph();
            g.NamespaceMap.AddNamespace("semstat", new Uri("http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#"));
            g.NamespaceMap.AddNamespace("void", new Uri("http://rdfs.org/ns/void#"));
            var obj = new Object();
            var failedEndpoints = new List<string>();
            // foreach (var endpoint in endpoints)
            Parallel.ForEach(endpoints, endpoint =>
            {
                try
                {
                    var remoteEndpoint = new SparqlRemoteEndpoint(new Uri(endpoint));
                    var dsRes = remoteEndpoint.QueryWithResultSet("PREFIX void:<http://rdfs.org/ns/void#> SELECT ?ds WHERE { ?ds a void:Dataset . }");
                    INode ds;
                    if (dsRes.IsEmpty)
                    {
                        ds = g.CreateBlankNode();
                    }
                    else
                    {
                        ds = (IUriNode)dsRes.First()["ds"];
                    }
                    foreach (var selectQuery in queries)
                    // queries.AsParallel().Select(selectQuery => 
                    {
                        var results = remoteEndpoint.QueryWithResultSet(selectQuery);
                        if (results.IsEmpty)
                        {
                            // return false;
                            continue;
                        }
                        // ?definitionsCount ?triples ?feature
                        var definitionsCount = int.Parse(((ILiteralNode)results.First()["definitionsCount"]).Value);
                        if (definitionsCount <= 0)
                        {
                            // return false;
                            continue;
                        }
                        var feature = ((IUriNode)results.First()["feature"]).Uri;
                        var triples = results.First().Variables.Contains("triples") ? int.Parse(((ILiteralNode)results.First()["triples"]).Value) : 0;
                        lock (obj)
                        {          
                            var stat = g.CreateBlankNode();                  
                            g.Assert(ds, g.CreateUriNode("semstat:hasStat"), stat);
                            g.Assert(ds, g.CreateUriNode("void:sparqlEndpoint"), g.CreateUriNode(new Uri(endpoint)));
                            g.Assert(stat, g.CreateUriNode("semstat:hasSemanticFeature"), g.CreateUriNode(feature));
                            g.Assert(stat, g.CreateUriNode("semstat:definitionCount"), 
                                g.CreateLiteralNode(definitionsCount.ToString(), 
                                new Uri("http://www.w3.org/2001/XMLSchema#integer")));
                            if (triples > 0)
                            {                        
                                g.Assert(stat, g.CreateUriNode("semstat:usageCount"), 
                                    g.CreateLiteralNode(triples.ToString(), 
                                    new Uri("http://www.w3.org/2001/XMLSchema#integer")));
                            }
                        }
                        // return true;
                    }//).ToList();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex);
                    lock (obj)
                    {
                        failedEndpoints.Add(endpoint);
                    }
                }
            });
            var writer = new CompressingTurtleWriter{PrettyPrintMode = true};
            writer.Save(g, @"C:\dev\dotnet\OntoSemStatsWeb\UnitTests\results.ttl");
            Console.WriteLine("saved!");
            foreach (var endpoint in failedEndpoints)
            {
                Console.WriteLine(endpoint);
            }
        }
        [Fact]
        public void GetSparqlEndpointFromLODCloud()
        {
            var results = new List<string>();
            var jsonString = System.IO.File.ReadAllText(@"C:\dev\dotnet\OntoSemStatsWeb\OntoSemStatsJS\lod-data.json");
            using (var document = JsonDocument.Parse(jsonString))
            {
                var root = document.RootElement;
                foreach (var datasetName in root.EnumerateObject())
                {
                    var dataset = root.GetProperty(datasetName.Name);
                    var sparql = dataset.GetProperty("sparql");
                    var count = sparql.GetArrayLength();
                    if (count <= 0) continue;
                    foreach (var endpoint in sparql.EnumerateArray())
                    {
                        var status = endpoint.GetProperty("status");
                        if (status.GetString() == "OK")
                        {
                            var accessUrl = endpoint.GetProperty("access_url");
                            results.Add(accessUrl.GetString());
                        }
                    }
                }
            }
            System.IO.File.WriteAllLines(@"C:\dev\dotnet\OntoSemStatsWeb\UnitTests\endpoints2", results);
        }

        [Fact]
        public void TestName2()
        {
            var g = new Graph();
            g.LoadFromFile(@"C:\dev\dotnet\OntoSemStatsWeb\UnitTests\results.ttl");
            var sparqlEndpoint = g.CreateUriNode("void:sparqlEndpoint");
            var count = g.GetTriplesWithPredicate(sparqlEndpoint).Count();
            Console.WriteLine(count);
            var triples = g.GetTriplesWithPredicate(g.CreateUriNode("semstat:hasSemanticFeature"));
            var dict = new Dictionary<string, Dictionary<string, long>>();
            foreach (var triple in triples)
            {
                var feature = ((IUriNode)triple.Object).Uri.ToString();
                if (!dict.ContainsKey(feature))
                {
                    dict[feature] = new Dictionary<string, long>
                    {
                        {"count" , 1}
                    };
                }
                else
                {
                    dict[feature]["count"]++;
                }
                var definition = (ILiteralNode)g.GetTriplesWithSubjectPredicate(triple.Subject, g.CreateUriNode("semstat:definitionCount")).First().Object;
                dict[feature]["definition"] = dict[feature].ContainsKey("definition") ? int.Parse(definition.Value) + dict[feature]["definition"] : int.Parse(definition.Value);
                var usage = (ILiteralNode)g.GetTriplesWithSubjectPredicate(triple.Subject, g.CreateUriNode("semstat:usageCount")).FirstOrDefault()?.Object;
                if (usage != null)
                {
                    dict[feature]["usage"] = dict[feature].ContainsKey("usage") ? int.Parse(usage.Value) + dict[feature]["usage"] : int.Parse(usage.Value);
                }
            }
            var writerOptions = new JsonWriterOptions
            {
                Indented = true
            };
            using (var fs = System.IO.File.Create(@"C:\dev\dotnet\OntoSemStatsWeb\UnitTests\stats.json"))
            using (var writer = new Utf8JsonWriter(fs, writerOptions))
            {                
                JsonSerializer.Serialize(writer, dict);
            }
            var query = dict.Select(x => new {x.Key, definition = x.Value["definition"]}).OrderByDescending(x => x.definition);
            foreach (var q in query)
            {
                Console.WriteLine($"{q.Key} : {q.definition}");
            }
            // 95
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#RdfType : 6520629404
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlSameAs : 239059721
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#RdfsSubClassOf : 70336931
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlEquivalentClass : 2435379
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#RdfsRange : 420953
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#RdfsDomain : 227241
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlDifferentFrom : 105677
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlEquivalentProperty : 45131
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlDisjointWith : 36667
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlFunctionalProperty : 23133
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlInverseOf : 17824
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlPropertyChainAxiom : 7430
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlWithRestrictions : 5243
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlAllDisjointClasses : 1751
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlTransitiveProperty : 1334
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlSymmetricProperty : 1073
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlPropertyDisjointWith : 1054
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlAllDifferent : 1051
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlInverseFunctionalProperty : 640
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlAsymmetricProperty : 334
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlIrreflexiveProperty : 320
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlDisjointUnionOf : 226
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#RdfsSubProperty : 98
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlReflexiveProperty : 94
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlAllDisjointProperties : 16
            Console.WriteLine("---------------------------------------");
            var query2 = dict.Where(x => x.Value.ContainsKey("usage")).Select(x => new {x.Key, usage = x.Value["usage"]}).OrderByDescending(x => x.usage);
            foreach (var q in query2)
            {
                Console.WriteLine($"{q.Key} : {q.usage}");
            }
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlSymmetricProperty : 1822213443
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlFunctionalProperty : 1038751396
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlTransitiveProperty : 94358694
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlInverseFunctionalProperty : 9774073
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlIrreflexiveProperty : 5619379
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlReflexiveProperty : 874350
            // http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#OwlAsymmetricProperty : 1075
        }

        [Fact]
        public async Task GraphViz()
        {
            
            var g = new Graph();
            g.Assert(
                g.CreateUriNode(new Uri("http://example.org/Test1")), 
                g.CreateUriNode(new Uri("http://example.org/get")), 
                g.CreateUriNode(new Uri("http://example.org/Test2")));

            var gs = new GraphVizGenerator("svg", @"C:\Program Files (x86)\Graphviz2.38\bin");
            var fn = @"C:\dev\dotnet\OntoSemStatsWeb\UnitTests\img.svg";
            gs.Generate(g, fn, false);
            var text = await System.IO.File.ReadAllTextAsync(fn);
            var doc = XDocument.Parse(text);  
            Console.WriteLine(doc.Root.ToString());
        }
    }
}
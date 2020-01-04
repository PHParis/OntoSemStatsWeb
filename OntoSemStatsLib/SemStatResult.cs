using System;
using System.IO;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace OntoSemStatsLib
{
    public class SemStatsResult
    {
        public string ErrorMessage {get;set;}
        /// <summary>
        /// Date of generation.
        /// </summary>
        /// <value></value>
        public DateTime Date {get;set;}

        public IGraph Instance {get;set;}

        public string ToTurtle()
        {
            var wr = new CompressingTurtleWriter();
            return VDS.RDF.Writing.StringWriter.Write(Instance, wr);
        }
        public string ToNTriples()
        {            
            var wr = new NTriplesWriter();
            return VDS.RDF.Writing.StringWriter.Write(Instance, wr);
        }
        public string ToNotation3()
        {            
            var wr = new Notation3Writer();
            return VDS.RDF.Writing.StringWriter.Write(Instance, wr);
        }
        public string ToJsonLd()
        {            
            var wr = new JsonLdWriter();
            var store = new TripleStore();
            store.Add(Instance);
            return VDS.RDF.Writing.StringWriter.Write(store, wr);
        }

        public string ToRdfXmlWriter()
        {
            var wr = new RdfXmlWriter();
            return VDS.RDF.Writing.StringWriter.Write(Instance, wr);
        }

        public static SemStatsResult Get(string endpoint)
        {
            var result = new SemStatsResult();
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                result.ErrorMessage = "You must provide a valid SPARQL endpoint URI!";
                return result;
            }
            var g = new Graph();
            result.Instance = g;
            result.Date = DateTime.Now;
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
            }
            catch (System.Exception ex)
            {
                result.ErrorMessage = ex.ToString();
            }
            return result;
        }
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
    }
}
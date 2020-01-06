const queries = [
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#> 
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
}`
,
`PREFIX :<http://AAAAAA/ontologies/OntoSemStats.owl#>
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
}`
];
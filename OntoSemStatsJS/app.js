const myEngine = Comunica.newEngine();
const sourceType = "sparql";
var results = new Array();

document.addEventListener("DOMContentLoaded", function() {
    var queryEndpoint = document.getElementById("queryEndpoint");
    queryEndpoint.onclick = displayQuery;
});

function displayQuery() {
    var endpointInput = document.getElementById("endpoint");
    var isValidEndpoint = endpointInput.checkValidity();
    if (isValidEndpoint) {
        var endpoint = endpointInput.value;
        var query = `PREFIX :<http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#>
        PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#>
        PREFIX owl:<http://www.w3.org/2002/07/owl#>
        PREFIX void:<http://rdfs.org/ns/void#>
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
                    UNION
                    {
                        ?p2 a owl:FunctionalProperty .
                        ?p (rdfs:subPropertyOf|owl:equivalentProperty)+ ?p2 .
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
                            UNION
                            {
                                ?p2 a owl:FunctionalProperty .
                                ?p (rdfs:subPropertyOf|owl:equivalentProperty)+ ?p2 .                        
                            } 
                        }
                    }
                    ?s ?p ?o .
                }
            }
        }`;
        // execQuery("http://dbpedia.org/sparql", query);
        execQuery(endpoint, query);
    }    
}

function execQuery(endpoint, query) {
    results.length = 0
    myEngine
    .query(
        query,
      {
        sources: [
          {
            type: sourceType,
            value: endpoint
          }
        ]
      }
    )
    .then(function(result) {
      result.bindingsStream.on("data", function(data) {
        console.log(data.toObject());
        results.push(data.toObject());
      });
    });
}

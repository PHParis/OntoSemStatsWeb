const myEngine = Comunica.newEngine();
const sourceType = "sparql";
var results = new Array();
// var ds;

document.addEventListener("DOMContentLoaded", function() {
    var queryEndpoint = document.getElementById("queryEndpoint");
    queryEndpoint.onclick = displayQuery;
});

async function getDatasetName(endpoint) {
    var askResult = await myEngine.query('PREFIX void:<http://rdfs.org/ns/void#> ASK { ?ds a void:Dataset . }',
    {
      sources: [ { type: sourceType, value: endpoint } ]
    });
    var isPresent = await askResult.booleanResult;
    console.log("ASK dataset: " + isPresent);
    if (isPresent) {
        myEngine.query('PREFIX void:<http://rdfs.org/ns/void#> SELECT ?ds WHERE { ?ds a void:Dataset . }',
        {
          sources: [ { type: sourceType, value: endpoint } ]
        }).then(function(result) {
            result.bindingsStream.on('data', function (data) {
                var ds = data.toObject()["?ds"].value;
                execQuery(endpoint, ds);         
            });
        }).catch(function() {
          // An error occurred
        });
    } else {
        execQuery(endpoint, "_:b0");
    }
    // myEngine.query('PREFIX void:<http://rdfs.org/ns/void#> SELECT ?ds WHERE { ?ds a void:Dataset . }',
    // {
    //   sources: [ { type: sourceType, value: endpoint } ]
    // }).then(function(result) {
    //     console.log(result);
    //     console.log(result.booleanResult);
    //     console.log(result.bindingsStream);
    //     console.log(result.bindingsStream._events);
    //     result.bindingsStream.on('data', function (data) {
    //         console.log(result.bindingsStream._events);
    //         resultsFound = false;
    //         var ds = data.toObject()["?ds"].value;
    //         console.log("ds 2 : " + ds);  
    //         execQuery(endpoint, ds);         
    //     });        
    //     console.log(result.bindingsStream._events.readable);
    // }).catch(function() {
    //   // An error occurred
    // });
    // return "_:b0"
    
    // result.bindingsStream.on('data', function (data) {
    //   var ds = data.toObject()["?ds"].value;
    //   console.log("ds 2 : " + ds);  
    // //   execQuery(endpoint, ds);
    // });

}

function displayQuery() {
    var endpointInput = document.getElementById("endpoint");
    var isValidEndpoint = endpointInput.checkValidity();
    if (isValidEndpoint) {
        var endpoint = endpointInput.value;

        getDatasetName(endpoint);
        
        // execQuery(endpoint, "_:b0");
    }   
    return false; 
}

function execQuery(endpoint, ds) {
    console.log(ds);  
    console.log("test2");
    results.length = 0;    
    document.getElementById('table_result').getElementsByTagName('tbody')[0].innerHTML = "";
    document.getElementById('n_triples_results').innerHTML = "";
    queries.forEach((query, i) =>    
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
            var dataObj = data.toObject();
            console.log(dataObj);
            results.push(dataObj);
            addNewLineInTable(dataObj);
            displayNTriples(dataObj, i + 1);
        });
        })
    );
}

function displayNTriples(data, i) {
    if (data["?definitionsCount"].value == "0") {
        return;
    }
    var n_triples_results = document.getElementById('n_triples_results');
    n_triples_results.innerHTML += "<br>_:b" + i + " a &lt;http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#Stat&gt; . <br\>";
    n_triples_results.innerHTML += "_:b" + i + " &lt;http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#hasSemanticFeature&gt; &lt;" + data["?feature"].value + " . <br\>";
    n_triples_results.innerHTML += "_:b" + i + " &lt;http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#definitionCount&gt; " + data["?definitionsCount"].value + " . <br\>";
    if (typeof data["?triples"] !== 'undefined') {
        n_triples_results.innerHTML += "_:b" + i + " &lt;http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl#usageCount&gt; " + data["?triples"].value + " .";
    }    
}

function addNewLineInTable(data) {

    if (data["?definitionsCount"].value == "0") {
        return;
    }

    var tableRef = document.getElementById('table_result').getElementsByTagName('tbody')[0];

    // Insert a row in the table at the last row
    var newRow = tableRef.insertRow();

    // Insert a cell in the row at index 0
    var newCell = newRow.insertCell(0);

    // Append a text node to the cell
    var newText = document.createTextNode(data["?feature"].value);
    newCell.appendChild(newText);

    // Insert a cell in the row at index 0
    var newCell = newRow.insertCell(1);

    // Append a text node to the cell
    var newText = document.createTextNode(data["?definitionsCount"].value);
    newCell.appendChild(newText);

    var usageCount = "NA";
    if (typeof data["?triples"] !== 'undefined') {
        usageCount = data["?triples"].value;
    }

    // Insert a cell in the row at index 0
    var newCell = newRow.insertCell(2);

    // Append a text node to the cell
    var newText = document.createTextNode(usageCount);
    newCell.appendChild(newText);
}
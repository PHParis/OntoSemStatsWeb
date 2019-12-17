const myEngine = Comunica.newEngine();
const sourceType = "sparql";
var results = new Array();

query("http://dbpedia.org/sparql", `SELECT * { ?s ?p <http://dbpedia.org/resource/Belgium>. ?s ?p ?o } LIMIT 100`);

function query(endpoint, query) {
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

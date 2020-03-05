# OntoSemStats

[OntoSemStats](http://cedric.cnam.fr/isid/ontologies/OntoSemStats.owl) is an ontology which provide statistics about
semantics usage on an RDF knowledge graph.
We provide a live demonstrator [here](https://ontosemstats.herokuapp.com/).

## Web application

To build the Web application using Docker, you can run this
command:

- `docker build -f web.Dockerfile -t semstatsweb .`

Then, to run the container, use this command:

- `docker run -it --rm -p 5000:80 semstatsweb`

Then, go to [http://localhost:5000](http://localhost:5000)
and try our tool.

You can also use the Web API. For example, by going to
[http://localhost:5000/api/instance?endpoint=http://dbpedia.org/sparql](http://localhost:5000/api/instance?endpoint=http://dbpedia.org/sparql),
you will obtain statistics about DBpedia.

## Command-line application

To build the command-line application using Docker, you can
run this command:

- `docker build -f cmd.Dockerfile -t semstatscmd .`

Then, to run the container and retrieve information about DBpedia,
run this command:

- `docker run -v ${PWD}:/data --rm -it semstatscmd -e http://dbpedia.org/sparql -o /data/semstat_dbpedia.ttl -f ttl`

By runing `docker run --rm -it semstatscmd --help`, you can
get further information about the command-line application.

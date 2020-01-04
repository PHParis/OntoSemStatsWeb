using System;
using System.IO;
using VDS.RDF;
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
    }
}
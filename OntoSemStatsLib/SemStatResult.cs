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
    }
}
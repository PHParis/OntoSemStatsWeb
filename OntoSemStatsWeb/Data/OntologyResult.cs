using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OntoSemStatsWeb.Data
{
    public class OntologyResult
    {
        [Required]
        [Url]
        public string SparqlEndpointUri { get; set; }
        // public string Result { get; set; }
        public string Turtle { get; set; }

        public Dictionary<string, Dictionary<string, string>> Result { get; set; }
    }
}

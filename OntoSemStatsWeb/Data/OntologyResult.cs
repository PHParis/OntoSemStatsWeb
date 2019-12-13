using System;
using System.ComponentModel.DataAnnotations;

namespace OntoSemStatsWeb.Data
{
    public class OntologyResult
    {
        [Required]
        [Url]
        public string SparqlEndpointUri { get; set; }
        public string Result { get; set; }
    }
}

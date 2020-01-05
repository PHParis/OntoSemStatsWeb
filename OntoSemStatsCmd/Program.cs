using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using OntoSemStatsLib;

namespace OntoSemStatsCmd
{
    class Program
    {
        public class Options
        {
            [Option('e', "endpoint", Required = true, HelpText = "SPARQL endpoint URI.")]
            public string Endpoint { get; set; }

            [Option('o', "output", Required = true, HelpText = "Output file.")]
            public string FilePath { get; set; }

            [Option('f', "format", Required = true, HelpText = "RDF serialization format (between 'ttl', 'n3', 'nt', 'jsonld' and 'rdf').")]
            public string Format { get; set; }
        }
        static void RunOptions(Options opts)
        {
            //handle options
            Endpoint = opts.Endpoint;
            Format = opts.Format;
            FilePath = opts.FilePath;
            if (!formats.Contains(Format))
            {
                Console.WriteLine("You must provide a valid format between 'ttl', 'n3', 'nt', 'jsonld' and 'rdf'!");
                System.Environment.Exit(0);
            }
        }
        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
            System.Environment.Exit(0);
        }
        static string Endpoint;
        static string Format;
        static string FilePath;
        static readonly string[] formats = { "ttl", "n3", "nt", "jsonld", "rdf" };
        static async Task Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
                
            var semStatsResult = new SemStatsResult { Endpoint = Endpoint };
            Console.WriteLine($"Endpoint provided: {semStatsResult.Endpoint}");
            await Task.Factory.StartNew(semStatsResult.Get);
            if (!string.IsNullOrWhiteSpace(semStatsResult.ErrorMessage))
            {
                Console.WriteLine($"A problem occured while processing: {semStatsResult.ErrorMessage}");
                System.Environment.Exit(0);
            }
            if (semStatsResult.Instance != null && !semStatsResult.Instance.IsEmpty)
            {
                Console.WriteLine("Saving result graph...");
                var text = Format switch
                {
                    "n3" => semStatsResult.ToNotation3(),
                    "nt" => semStatsResult.ToNTriples(),
                    "jsonld" => semStatsResult.ToJsonLd(),
                    "rdf" => semStatsResult.ToRdfXml(),
                    _ => semStatsResult.ToTurtle()
                };
                await System.IO.File.WriteAllTextAsync(FilePath, text);
            }
            else
            {
                Console.WriteLine("Results are empty!");
            }
            Console.WriteLine("end!");
        }
    }
}

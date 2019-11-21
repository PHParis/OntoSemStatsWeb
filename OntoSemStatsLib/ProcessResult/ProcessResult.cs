using LanguageExt;
using static LanguageExt.Prelude;
using VDS.RDF.Query;
using System.Linq;
using System.Collections.Generic;

namespace OntoSemStatsLib.ProcessResult
{

    public class Trpl<TKey>
    {
        public TKey s { get; }
        public TKey p { get; }
        public TKey o { get; }

        public Trpl(TKey s, TKey p, TKey o)
        {
            this.s = s;
            this.p = p;
            this.o = o;
        }
    }

    public static class ProcessResult
    {
        public static readonly Dictionary<long, string> IdNodeIndex = new Dictionary<long, string>();
        public static readonly Dictionary<string, long> NodeIdIndex = new Dictionary<string, long>();
        public static Trpl<string> ToTrpl(this SparqlResult sparqlResult) => new Trpl<string>(
            s: sparqlResult["s"].ToString(), p: sparqlResult["p"].ToString(), o: sparqlResult["o"].ToString());

        public static Trpl<long> ToShortTriple(this Trpl<string> triple)
        {
            long sIndex;
            if (!NodeIdIndex.TryGetValue(triple.s, out sIndex))
            {
                sIndex = NodeIdIndex.LongCount() + 1;
                NodeIdIndex[triple.s] = sIndex;
                IdNodeIndex[sIndex] = triple.s;
            }

            long pIndex;
            if (!NodeIdIndex.TryGetValue(triple.p, out pIndex))
            {
                pIndex = NodeIdIndex.LongCount() + 1;
                NodeIdIndex[triple.p] = pIndex;
                IdNodeIndex[pIndex] = triple.p;
            }

            long oIndex;
            if (!NodeIdIndex.TryGetValue(triple.o, out oIndex))
            {
                oIndex = NodeIdIndex.LongCount() + 1;
                NodeIdIndex[triple.o] = oIndex;
                IdNodeIndex[oIndex] = triple.o;
            }
            return new Trpl<long>(sIndex, pIndex, oIndex);
        }
        public static void ProcessSparqlResultSet(this SparqlResultSet sparqlResultSet)
        {
            var tmp = sparqlResultSet.Select(sparqlResult => sparqlResult.ToTrpl().ToShortTriple());
            foreach (var result in tmp)
            {

            }
        }

        public static (string s, string p, string o) ToTuple(SparqlResult sparqlResult) =>
            (
                s: sparqlResult["s"].ToString(),
                p: sparqlResult["p"].ToString(),
                o: sparqlResult["o"].ToString()
            );

        public static void ProcessSparqlResult(this SparqlResult sparqlResult)
        {
            var bool1 = "url" == "test.com" ? Some("html here") : None;
        }


    }
}
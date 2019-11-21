using LanguageExt;
using static LanguageExt.Prelude;
using VDS.RDF.Query;
using System.Linq;
using System.Collections.Generic;
using OntoSemStatsLib.Utils.Vocabularies;
using System;
using VDS.RDF.Ontology;

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

    public class BasicStat
    {
        public string Subject { get; }
        public string Property { get; }

        public Option<string> Typed { get; }
        public Option<string> Ranged { get; }
        public Option<string> Domained { get; }
        public Option<string> UsedClass { get; }

        public BasicStat(Trpl<string> t)
        {
            Subject = t.s;
            Property = t.p;
            Ranged = t.p == RDFS.PropertyRange.ToString() ?
                Some(t.s) : None;
            Domained = t.p == RDFS.PropertyDomain.ToString() ?
                Some(t.s) : None;
            (Typed, UsedClass) = t.p == RDF.PropertyType.ToString() ?
                (Some(t.s), Some(t.o)) : (None, None);
        }
    }

    public class ComplexStat
    {
        /// <summary>
        /// Number of triples.
        /// </summary>
        /// <value></value>
        public int TripleCount { get; }
        /// <summary>
        /// Number of distinct subjects.
        /// </summary>
        /// <value></value>
        public int DistinctSubjectCount { get; }
        /// <summary>
        /// Number of distinct subject with at least one explicit type.
        /// </summary>
        /// <value></value>
        public int DistinctSubjectWithExplicitTypeCount { get; }

        /// <summary>
        /// Number of distinct predicates used.
        /// </summary>
        /// <value></value>
        public int DistinctUsedPredicatesCount { get; }

        /// <summary>
        /// Number of defined properties, i.e. subject of type rdf:Property, owl:ObjectProperty, owl:DatatypeProperty.
        /// </summary>
        /// <value></value>
        public int DefinedPredicates { get; }

        /// <summary>
        /// Number of distinct properties with at least one range.
        /// </summary>
        /// <value></value>
        public int DistinctPropertyRangeCount { get; }

        /// <summary>
        /// Number of distinct properties with at least one domain.
        /// </summary>
        /// <value></value>
        public int DistinctPropertyDomainCount { get; }

        /// <summary>
        /// Number of triples with owl:equivalentProperty used as a predicate.
        /// </summary>
        /// <value></value>
        public int EquivalentPropertyCount { get; }
        public int InverseOfCount { get; }
        public int SameAsCount { get; }
        public int DifferentFromCount { get; }
        public int AllDifferentCount { get; }

        public int FunctionalPropertyDefinedCount { get; }
        public int FunctionalPropertyTripleCount { get; }
        public int FunctionalPropertySubjectCount { get; }

        public ComplexStat(IEnumerable<BasicStat> basicStats)
        {
            TripleCount = basicStats.Count();
            DistinctSubjectCount = basicStats.Select(x => x.Subject).Distinct().Count();
            DistinctSubjectWithExplicitTypeCount = basicStats.Where(x => x.Typed.IsSome)
                .Select(x => x.Typed)
                .Distinct().Count();
            DistinctUsedPredicatesCount = basicStats.Select(x => x.Property).Distinct().Count();
            DistinctPropertyRangeCount = basicStats.Where(x => x.Ranged.IsSome)
                .Select(x => x.Ranged)
                .Distinct().Count();
            DistinctPropertyDomainCount = basicStats.Where(x => x.Domained.IsSome)
                .Select(x => x.Domained)
                .Distinct().Count();
            EquivalentPropertyCount = basicStats.Count(x =>
                x.Property == OntologyHelper.PropertyEquivalentProperty);
            InverseOfCount = basicStats.Count(x =>
                x.Property == OntologyHelper.PropertyInverseOf);
            SameAsCount = basicStats.Count(x =>
                x.Property == OntologyHelper.PropertySameAs);

            DifferentFromCount = basicStats.Count(x =>
                x.Property == OntologyHelper.PropertyDifferentFrom);

            AllDifferentCount = basicStats.Where(x => x.UsedClass.IsSome).Count(x =>
                x.UsedClass == OntologyHelper.PropertyDifferentFrom);
            FunctionalPropertyDefinedCount = basicStats.Where(x => x.UsedClass.IsSome).Count(x =>
                x.UsedClass == OWL.ClassFunctionalProperty.ToString());
        }
    }

    public static class ProcessResult
    {

        public static BasicStat ToBasicStat(this Trpl<string> t)
        {
            // var usedProps = new System.Collections.Generic.HashSet<string> { t.p };
            // var usedSubjects = new System.Collections.Generic.HashSet<string> { t.s };
            // Option<BasicStat> basicStat = t.p switch
            // {
            //     OntologyHelper.PropertyDomain => {new BasicStat()},
            //     _ => None
            // };
            // switch (t.p)
            // {
            //     case OntologyHelper.PropertyType:
            //         var typed = t.p == RDF.PropertyType.ToString() ? Some(t.s) : None;
            //         break;
            //     case OntologyHelper.PropertyDomain:
            //         var domained = t.p == RDFS.PropertyDomain.ToString() ? Some(t.s) : None;
            //         break;
            //     case OntologyHelper.PropertyRange:
            //         var ranged = t.p == RDFS.PropertyRange.ToString() ? Some(t.s) : None;
            //         break;                
            //     case OntologyHelper.PropertySubClassOf:
            //         break;
            //     case OntologyHelper.PropertySubPropertyOf:
            //         break;
            //     default:
            //         // basicStat = None;
            //         break;
            // };
            // var subClassOf = t.p == RDFS.PropertySubClassOf.ToString() ? Some(t.s) : None;
            // var ranged = t.p == RDFS.PropertyRange.ToString() ? Some(t.s) : None;
            var ranged = t.p == RDFS.PropertyRange.ToString() ? Some(t.s) : None;
            var domained = t.p == RDFS.PropertyDomain.ToString() ? Some(t.s) : None;
            var typed = t.p == RDF.PropertyType.ToString() ? Some(t.s) : None;

            // rdfs:subClassOf
            // rdfs:subPropertyOf
            // return basicStat;
            throw new NotImplementedException();
        }

        public static void Algo()
        {
            var basicStats = new List<BasicStat>();
            foreach (var basicStat in basicStats)
            {

            }
            // filter certaines props de RDF, RDFS ou OWL ?
            // Par exemple, on s'en fiche de savoir que 
            // rdf:type n'est pas défini dans le graphe courant

            // transformer en index les 3 éléments ?
            // augmenter de 1 le nombre de triplets
            // ajouter p au propriétés utilisées (hashset)
            // ajouter s aux sujets distincs (hashset)
            // si p = range ajouter s aux props avec range
            // si p = domain ajouter s au props avec domain
            // si p = type, ajouter s aux sujets avec type explicite
            //      ajouter o aux classes utilisées
            //      Si o est propertydefinition (genre owl:Functional, faire un truc)
            //          ajouter s au Functional ou Reflexive dans un dico et un hashet
            //      Si o est ClasseDefiniton
            //          ajouter s dans la bonne case
            // si p est une propriété identité (sameAs ou identity) gérer
        }
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
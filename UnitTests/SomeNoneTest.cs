using LanguageExt;
using static LanguageExt.Prelude;
using Xunit;
using OntoSemStatsLib.ProcessResult;
using VDS.RDF.Ontology;

namespace UnitTests
{
    public class UnitTest2
    {
        [Fact]
        public void TestName()
        {
            var t1 = new Trpl<string>("a", "b", "c");
            Option<BasicStat> basicStat = t1.p switch
            {
                OntologyHelper.PropertyDomain => new BasicStat(),
                _ => None
            };
            Assert.True(basicStat.IsNone);

            var t2 = new Trpl<string>("a", OntologyHelper.PropertyDomain, "c");
            Option<BasicStat> basicStat2 = t2.p switch
            {
                OntologyHelper.PropertyDomain => new BasicStat(),
                _ => None
            };
            Assert.True(basicStat2.IsSome);

            var t3 = new Trpl<string>("a", OntologyHelper.PropertyDomain, "c");
            var basicStat3 = t3.p switch
            {
                OntologyHelper.PropertyDomain => Some(new BasicStat()),
                _ => None
            };
            Assert.True(basicStat3.IsSome);

        }

    }
}
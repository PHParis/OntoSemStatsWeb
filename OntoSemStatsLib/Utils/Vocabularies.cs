using System;
using VDS.RDF;
using VDS.RDF.Ontology;

namespace OntoSemStatsLib.Utils.Vocabularies
{
    public static class XSD
    {
        public static readonly String NS_URI = NamespaceMapper.XMLSCHEMA;
        public static readonly Uri string_ = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "string");

        public static readonly Uri DatatypeDecimal = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "decimal");
        public static readonly Uri DatatypeDuration = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "duration");
        public static readonly Uri DatatypeGDay = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "gDay");
        public static readonly Uri DatatypeGMonth = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "gMonth");
        public static readonly Uri DatatypeGMonthDay = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "gMonthDay");
        public static readonly Uri DatatypeGYear = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "gYear");
        public static readonly Uri DatatypeGYearMonth = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "gYearMonth");
        public static readonly Uri DatatypeInteger = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "integer");
        public static readonly Uri DatatypeNegativeInteger = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "negativeInteger");
        public static readonly Uri DatatypeNonNegativeInteger = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "nonNegativeInteger");
        public static readonly Uri DatatypeNonPositiveInteger = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "nonPositiveInteger");
        public static readonly Uri DatatypePositiveInteger = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "positiveInteger");
        public static readonly Uri DatatypeUnsignedByte = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "unsignedByte");
        public static readonly Uri DatatypeUnsignedInt = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "unsignedInt");
        public static readonly Uri DatatypeUnsignedLong = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "unsignedLong");
        public static readonly Uri DatatypeUnsignedShort = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "unsignedShort");
        public static readonly Uri DatatypeByte = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "byte");
        public static readonly Uri DatatypeDouble = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "double");
        public static readonly Uri DatatypeFloat = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "float");
        public static readonly Uri DatatypeInt = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "int");
        public static readonly Uri DatatypeLong = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "long");
        public static readonly Uri DatatypeShort = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "short");

        public static readonly Uri DatatypeAnySimpleType = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "anySimpleType");
        public static readonly Uri DatatypeAnyURI = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "anyURI");
        public static readonly Uri DatatypeBase64Binary = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "base64Binary");
        public static readonly Uri DatatypeDate = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "date");
        public static readonly Uri DatatypeDateTime = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "dateTime");
        public static readonly Uri DatatypeENTITY = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "ENTITY");
        public static readonly Uri DatatypeHexBinary = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "hexBinary");
        public static readonly Uri DatatypeID = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "ID");
        public static readonly Uri DatatypeIDREF = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "IDREF");
        public static readonly Uri PropertyLanguage = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "language");
        public static readonly Uri DatatypeName = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "Name");
        public static readonly Uri DatatypeNCName = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "NCName");
        public static readonly Uri DatatypeNMTOKEN = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "NMTOKEN");
        public static readonly Uri DatatypeNormalizedString = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "normalizedString");
        public static readonly Uri DatatypeNOTATION = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "NOTATION");
        public static readonly Uri DatatypeQName = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "QName");
        public static readonly Uri DatatypeTime = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "time");
        public static readonly Uri DatatypeToken = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "token");
        public static readonly Uri DatatypeBoolean = UriFactory.Create(NamespaceMapper.XMLSCHEMA + "boolean");

    }

    public static class RDF
    {
        public static readonly String NS_URI = NamespaceMapper.RDF;

        public static readonly Uri ClassResource = UriFactory.Create(NamespaceMapper.RDF + "Resource");
        public static readonly Uri ClassHTML = UriFactory.Create(RDF.NS_URI + "HTML");
        public static readonly Uri ClassPlainLiteral = UriFactory.Create(RDF.NS_URI + "PlainLiteral");
        public static readonly Uri ClassXMLLiteral = UriFactory.Create(NamespaceMapper.RDF + "XMLLiteral");

        public static readonly Uri PropertyLangString = UriFactory.Create(RDF.NS_URI + "langString");

        public static readonly Uri PropertyGraphLabel = UriFactory.Create(RDF.NS_URI + "graphLabel");
        public static readonly Uri PropertySubject = UriFactory.Create(RDF.NS_URI + "subject");
        public static readonly Uri PropertyPredicate = UriFactory.Create(RDF.NS_URI + "predicate");
        public static readonly Uri PropertyObject = UriFactory.Create(RDF.NS_URI + "object");

        public static readonly Uri Nil = UriFactory.Create(NamespaceMapper.RDF + "nil");
        public static readonly Uri PropertyFirst = UriFactory.Create(NamespaceMapper.RDF + "first");
        public static readonly Uri PropertyRest = UriFactory.Create(NamespaceMapper.RDF + "rest");
        public static readonly Uri PropertyType = UriFactory.Create(OntologyHelper.PropertyType);
    }

    public static class RDFS
    {
        public static readonly String NS_URI = NamespaceMapper.RDFS;

        public static readonly Uri Class = UriFactory.Create(OntologyHelper.RdfsClass);
        public static readonly Uri PropertyComment = UriFactory.Create(OntologyHelper.PropertyComment);
        public static readonly Uri ClassDatatype = UriFactory.Create(NamespaceMapper.RDFS + "Datatype");
        public static readonly Uri PropertyDomain = UriFactory.Create(OntologyHelper.PropertyDomain);
        public static readonly Uri PropertyIsDefinedBy = UriFactory.Create(OntologyHelper.PropertyIsDefinedBy);
        public static readonly Uri PropertyLabel = UriFactory.Create(OntologyHelper.PropertyLabel);
        public static readonly Uri ClassLiteral = UriFactory.Create(NamespaceMapper.RDFS + "Literal");
        public static readonly Uri ClassProperty = UriFactory.Create(OntologyHelper.RdfProperty);
        public static readonly Uri PropertyRange = UriFactory.Create(OntologyHelper.PropertyRange);
        public static readonly Uri ClassResource = UriFactory.Create(NamespaceMapper.RDFS + "Resource");
        public static readonly Uri PropertySeeAlso = UriFactory.Create(OntologyHelper.PropertySeeAlso);
        public static readonly Uri PropertySubClassOf = UriFactory.Create(OntologyHelper.PropertySubClassOf);
        public static readonly Uri PropertySubPropertyOf = UriFactory.Create(OntologyHelper.PropertySubPropertyOf);
    }

    public static class OWL
    {
        public static readonly String NS_URI = NamespaceMapper.OWL;


        public static readonly Uri PropertyAllValuesFrom = UriFactory.Create(NS_URI + "allValuesFrom");
        public static readonly Uri ClassAnnotationProperty = UriFactory.Create(OntologyHelper.OwlAnnotationProperty);
        public static readonly Uri Class = UriFactory.Create(OntologyHelper.OwlClass);
        public static readonly Uri PropertyCardinality = UriFactory.Create(NS_URI + "cardinality");
        public static readonly Uri ClassDatatypeProperty = UriFactory.Create(OntologyHelper.OwlDatatypeProperty);
        public static readonly Uri PropertyDifferentFrom = UriFactory.Create(OntologyHelper.PropertyDifferentFrom);
        public static readonly Uri PropertyDisjointWith = UriFactory.Create(OntologyHelper.PropertyDisjointWith);
        public static readonly Uri PropertyEquivalentClass = UriFactory.Create(OntologyHelper.PropertyEquivalentClass);
        public static readonly Uri PropertyEquivalentProperty = UriFactory.Create(OntologyHelper.PropertyEquivalentProperty);
        public static readonly Uri ClassFunctionalProperty = UriFactory.Create(NS_URI + "FunctionalProperty");
        public static readonly Uri PropertyImports = UriFactory.Create(OntologyHelper.PropertyImports);
        public static readonly Uri PropertyIncompatibleWith = UriFactory.Create(OntologyHelper.PropertyIncompatibleWith);
        public static readonly Uri PropertyInverseOf = UriFactory.Create(OntologyHelper.PropertyInverseOf);
        public static readonly Uri PropertyMaxCardinality = UriFactory.Create(NS_URI + "maxCardinality");
        public static readonly Uri PropertyMinCardinality = UriFactory.Create(NS_URI + "minCardinality");
        public static readonly Uri Nothing = UriFactory.Create(NS_URI + "Nothing");
        public static readonly Uri ClassObjectProperty = UriFactory.Create(OntologyHelper.OwlObjectProperty);
        public static readonly Uri PropertyOnProperty = UriFactory.Create(NS_URI + "onProperty");
        public static readonly Uri ClassOntology = UriFactory.Create(OntologyHelper.OwlOntology);
        public static readonly Uri PropertyPriorVersion = UriFactory.Create(OntologyHelper.PropertyPriorVersion);
        public static readonly Uri PropertySameAs = UriFactory.Create(OntologyHelper.PropertySameAs);
        public static readonly Uri SymmetricProperty = UriFactory.Create(NS_URI + "SymmetricProperty");
        public static readonly Uri Thing = UriFactory.Create(NS_URI + "Thing");
        public static readonly Uri PropertyVersionInfo = UriFactory.Create(OntologyHelper.PropertyVersionInfo);
    }

}
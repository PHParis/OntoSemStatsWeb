using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using OntoSemStatsLib;

namespace OntoSemStatsWeb.Formatters
{
    public class RdfOutputFormatter : TextOutputFormatter
    {
        public RdfOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/turtle"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/rdf+xml"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/n-triples"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/n3"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/ld+json"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type type)
        {
            if (typeof(SemStatsResult).IsAssignableFrom(type) 
                || typeof(IEnumerable<SemStatsResult>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            // var logger = serviceProvider.GetService(typeof(ILogger<VcardOutputFormatter>)) as ILogger;

            var response = context.HttpContext.Response;            

            Func<string, SemStatsResult, string> selectSerialization = (ct, sr) => ct switch
            {
                _ when ct.Contains("rdf") => sr.ToRdfXmlWriter(),
                _ when ct.Contains("triples") => sr.ToNTriples(),
                _ when ct.Contains("n3") => sr.ToNotation3(),
                _ when ct.Contains("ld") => sr.ToJsonLd(),
                _ => sr.ToTurtle()
            };

            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<SemStatsResult>)
            {
                foreach (SemStatsResult semStat in context.Object as IEnumerable<SemStatsResult>)
                {
                    // FormatVcard(buffer, semStat, logger);
                    var str = selectSerialization(response.ContentType, semStat);
                    buffer.AppendLine(str);
                }
            }
            else
            {
                var semStat = context.Object as SemStatsResult;
                var str = selectSerialization(response.ContentType, semStat);
                buffer.AppendLine(str);
                // FormatVcard(buffer, semStat, logger);                
            }
            await response.WriteAsync(buffer.ToString());
        }
    }
}
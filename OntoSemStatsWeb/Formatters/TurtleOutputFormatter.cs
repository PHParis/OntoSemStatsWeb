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
    public class TurtleOutputFormatter : TextOutputFormatter
    {
        public TurtleOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/turtle"));

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

            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<SemStatsResult>)
            {
                foreach (SemStatsResult semStat in context.Object as IEnumerable<SemStatsResult>)
                {
                    // FormatVcard(buffer, semStat, logger);
                    buffer.AppendLine(semStat.ToTurtle());
                }
            }
            else
            {
                var semStat = context.Object as SemStatsResult;
                buffer.AppendLine(semStat.ToTurtle());
                // FormatVcard(buffer, semStat, logger);                
            }
            await response.WriteAsync(buffer.ToString());
        }
    }
}
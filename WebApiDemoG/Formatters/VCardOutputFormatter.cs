using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiDemoG.Models;

namespace WebApiDemoG.Formatters
{
    public class VCardOutputFormatter : TextOutputFormatter
    {
        public VCardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var stringBuilder = new StringBuilder();
            if (context.Object is List<ContactModel> list)
            {
                foreach (var model in list)
                {
                    FormatVCard(stringBuilder, model);
                }
            }
            else 
            {
                var contact = context.Object as ContactModel;
                FormatVCard(stringBuilder, contact);
            }
            return response.WriteAsync(stringBuilder.ToString());
        }

        private static void FormatVCard(StringBuilder stringBuilder, ContactModel model)
        {
            stringBuilder.AppendLine("BEGIN:VCARD");
            stringBuilder.AppendLine("VERSION:2.1");
            stringBuilder.AppendLine($"FN:{model.Firstname}");
            stringBuilder.AppendLine($"LN:{model.Lastname}");
            stringBuilder.AppendLine($"PAN:{model.PAN}");
            stringBuilder.AppendLine($"UID:{model.Id}");
            stringBuilder.AppendLine("END:VCARD");
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(ContactModel).IsAssignableFrom(type) ||
                typeof(List<ContactModel>).IsAssignableFrom(type)) 
                return true;
            return false;
        }
    }
}

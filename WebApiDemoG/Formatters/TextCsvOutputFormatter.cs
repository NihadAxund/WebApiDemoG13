using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiDemoG.Models;

namespace WebApiDemoG.Formatters
{
    public class TextCsvOutputFormatter : TextOutputFormatter
    {
        public TextCsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var stringBuilder = new StringBuilder();
            if (context.Object is List<StudentModel> list)
            {
                AddProperties(stringBuilder, list[0]);
                foreach (var model in list)
                {
                    FormatTextCsv(stringBuilder, model);
                }
            }
            else
            {
                var model = context.Object as StudentModel;
                AddProperties(stringBuilder, model);
                FormatTextCsv(stringBuilder, model);
            }
            return response.WriteAsync(stringBuilder.ToString());
        }

        private static void AddProperties(StringBuilder stringBuilder, StudentModel model)
        {
            var properties = model.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (properties.Last() != property)
                    stringBuilder.Append(property.Name + " - ");
                else
                    stringBuilder.Append(property.Name + "\n");
            }
        }


        private static void FormatTextCsv(StringBuilder stringBuilder, StudentModel model)
        {
            stringBuilder.Append(model.Id + " - ");
            stringBuilder.Append(model.Fullname + " - ");
            stringBuilder.Append(model.SeriaNo + " - ");
            stringBuilder.Append(model.Age + " - ");
            stringBuilder.Append(model.Score + "\n");
        }   

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(StudentModel).IsAssignableFrom(type) ||
                typeof(List<StudentModel>).IsAssignableFrom(type))
                return true;
            return false;
        }
    }
}

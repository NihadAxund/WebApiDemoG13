using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiDemoG.Models;

namespace WebApiDemoG.Formatters
{
    public class TextCsvInputFormatter : TextInputFormatter
    {
        public TextCsvInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(StudentModel);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            using (var reader = new StreamReader(context.HttpContext.Request.Body, encoding))
            {
                var content = await reader.ReadToEndAsync();

                var studentData = content.Split('-').Select(s => s.Trim()).ToArray();

                if (studentData.Length != 5)
                {
                    // Invalid format, handle the error accordingly
                    return await InputFormatterResult.FailureAsync();
                }

                var studentModel = new StudentModel
                {
                    Fullname = studentData[1],
                    SeriaNo = studentData[2],
                    Age = int.Parse(studentData[3]),
                    Score = int.Parse(studentData[4])
                };

                return await InputFormatterResult.SuccessAsync(studentModel);
            }
        }
    }
}

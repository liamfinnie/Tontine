using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TontineService.ReferenceData.Models;

namespace TontineService.ReferenceData.Formatters
{
    public class CountryImageFormatter : MediaTypeFormatter
    {
        public CountryImageFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/png"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(Country);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream
            , HttpContent content, TransportContext transportContext)
        {
            var task = Task.Factory.StartNew(() =>
            {
                var country = value as Country;
                if (country == null) return;

                var image = Image.FromStream(new MemoryStream(country.Flag));
                image.Save(writeStream, ImageFormat.Png);
            });

            return task;
        }

    }
}
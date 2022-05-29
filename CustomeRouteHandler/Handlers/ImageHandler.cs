using ImageMagick;

namespace CustomeRouteHandler.Handlers
{
    public class ImageHandler
    {
        public RequestDelegate Handler(string filePath)
        {
            return async context =>
            {
                FileInfo fileInfo = new FileInfo($"{filePath}\\{context.Request.RouteValues["imageName"].ToString()}");

                using MagickImage magick = new MagickImage(fileInfo);

                int width = magick.Width, height = magick.Height;

                //Resim boyut kücültmeyi QueryString den aliyoruz. https://localhost:5001\\image-test.png?w=500&h=250

                if (!string.IsNullOrEmpty(context.Request.Query["w"].ToString()))
                    width = int.Parse(context.Request.Query["w"].ToString());

                if (!string.IsNullOrEmpty(context.Request.Query["h"].ToString()))
                    height = int.Parse(context.Request.Query["h"].ToString());

                magick.Resize(width, height); //Burada resmi boyutlarndiriyoruz.


                //Buradan itibaren resmi sayfaya response ederken ContentTypr ini ayarlamak kaliyor.
                var buffer = magick.ToByteArray(); //Elimizdeki resmi ByteArray formatinda elde edip, stream islemine hazirliyoruz
                context.Response.Clear(); //Gelen context icerisindeki veriyi temizliyoruz
                context.Response.ContentType = string.Concat("image/", fileInfo.Extension.Replace(".", ""));

                await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                await context.Response.WriteAsync(filePath);
            };
        }
    }
}

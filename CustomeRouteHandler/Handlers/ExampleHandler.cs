namespace CustomeRouteHandler.Handlers
{
    public class ExampleHandler
    {
        public RequestDelegate Handler()
        {
            return async context =>
            {
                await context.Response.WriteAsync("Hello World");
            };
        }
    }
}

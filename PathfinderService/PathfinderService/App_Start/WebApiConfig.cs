using Newtonsoft.Json;
using PathfinderService.Handlers;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.ExceptionHandling;

namespace PathfinderService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute(@"http://www.pathfinderparse.com/momster/parse", "Content-Type,content-type,Accept", "GET,HEAD,POST,DEBUG,PUT,OPTIONS");
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "PathfinderService");
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.UseFullTypeNameInSchemaIds();
                //c.DocumentFilter<SwaggerFilterPathItems>();
            })
            .EnableSwaggerUi(c => c.DisableValidator());

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            //to remove k__BackingField from results
            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

            //json only
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }

    public class SwaggerFilterPathItems : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths = swaggerDoc.paths.Where(entry => entry.Key.StartsWith("/apipath"))
                .ToDictionary(entry => entry.Key, entry => entry.Value);
        }
    }
}

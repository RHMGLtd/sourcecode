using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace coolbunny.common
{
    public class JsonModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (!IsJSONRequest(controllerContext))
            {
                return base.BindModel(controllerContext, bindingContext);
            }

            // Get the JSON data that's been posted
            var request = controllerContext.HttpContext.Request;
            var jsonStringData = new StreamReader(request.InputStream).ReadToEnd();

            // Use the built-in serializer to do the work for us
            return JsonConvert.DeserializeObject(jsonStringData, bindingContext.ModelMetadata.ModelType);
        }

        private static bool IsJSONRequest(ControllerContext controllerContext)
        {
            var contentType = controllerContext.HttpContext.Request.ContentType;
            return contentType.Contains("application/json");
        }
    }

    public class JsonAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }
    }
}
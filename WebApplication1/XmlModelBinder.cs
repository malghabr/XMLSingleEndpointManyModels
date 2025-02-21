using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebApplication1.Models;

public class XmlModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var request = bindingContext.HttpContext.Request;
        if (!request.ContentType.Equals("application/xml", System.StringComparison.OrdinalIgnoreCase))
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }

        using (var reader = new StreamReader(request.Body))
        {
            var xmlInput = await reader.ReadToEndAsync();
            object result;

            if (TryDeserializeXml<catalog>(xmlInput, out result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else if (TryDeserializeXml<Employees>(xmlInput, out result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }

    }

    private bool TryDeserializeXml<T>(string xmlInput, out object result)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xmlInput))
            {
                result = serializer.Deserialize(reader);
                return true;
            }
        }
        catch
        {
            result = null;
            return false;
        }
    }
}

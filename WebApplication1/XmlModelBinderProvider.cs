using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using WebApplication1.Models;

public class XmlModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(catalog) || context.Metadata.ModelType == typeof(Employees))
        {
            return new BinderTypeModelBinder(typeof(XmlModelBinder));
        }

        return null;
    }
}

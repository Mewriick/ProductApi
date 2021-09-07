using Alza.Product.Application.Patch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Reflection;

namespace Alza.Product.Api.Binders
{
    public class JsonMergePatchModelBindingProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType.GetTypeInfo().IsGenericType &&
                context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(JsonMergePatch<>))
            {
                var typeArgs = context.Metadata.ModelType.GetGenericArguments();
                var closedGenericType = typeof(JsonMergePatchModelBinder<>).MakeGenericType(typeArgs);
                return (IModelBinder)Activator.CreateInstance(closedGenericType)!;
            }

            return null!;
        }
    }
}

using Alza.Product.Application.Patch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Alza.Product.Api.Binders
{
    public class JsonMergePatchModelBinder<T> : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var jsonDocument = await JsonDocument.ParseAsync(bindingContext.HttpContext.Request.Body);
            var model = JsonMergePatch<T>.Create(jsonDocument);
            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;

namespace Alza.Product.Application.Patch
{
    public class JsonMergePatch<T>
    {
        private readonly JsonDocument json;

        private JsonMergePatch(JsonDocument json)
        {
            this.json = json ?? throw new System.ArgumentNullException(nameof(json));
        }

        public static JsonMergePatch<T> Create(JsonDocument json) => new JsonMergePatch<T>(json);

        public bool IsDefined<TProperty>(Expression<Func<T, TProperty>> propertyExpression, out TProperty value)
        {
            var propertyPath = GetPropertyPath(propertyExpression);
            var jsonElement = json.RootElement;
            var camelCase = false;

            while (propertyPath.Count > 0)
            {
                var propertyName = propertyPath.Pop();
                if (jsonElement.TryGetProperty(propertyName, out var childElement))
                {
                    jsonElement = childElement;
                    continue;
                }

                if (jsonElement.TryGetProperty(CamelCase(propertyName), out childElement))
                {
                    camelCase = true;
                    jsonElement = childElement;
                    continue;
                }

                value = default!;
                return false;
            }

            value = JsonSerializer.Deserialize<TProperty>(
                jsonElement.GetRawText(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = camelCase ? JsonNamingPolicy.CamelCase : null
                });

            return true;
        }

        private Stack<string> GetPropertyPath<TProperty>(Expression<Func<T, TProperty>> pathExpression)
        {
            var memberExpression = pathExpression.Body as MemberExpression;
            var names = new Stack<string>();

            while (memberExpression != null)
            {
                names.Push(memberExpression.Member.Name);
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            return names;
        }

        private static string CamelCase(string name)
        {
            if (name == null || name.Length == 0)
            {
                return name;
            }

            if (name.Length == 1)
            {
                return name.ToLower();
            }

            return char.ToLower(name[0]) + name.Substring(1);
        }
    }
}

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerNullableFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null) return;

        foreach (var property in schema.Properties)
        {
            // إذا كان الحقل يقبل null (مثل int?)
            if (property.Value.Nullable)
            {
                // إجبار المثال (Example) على أن يكون null بدلاً من 0
                property.Value.Example = new OpenApiNull();

                // إزالة أي قيمة افتراضية قد تكون عالقة
                property.Value.Default = null;
            }
        }
    }
}

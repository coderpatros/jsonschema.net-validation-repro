using System.Text.Json;
using Json.Schema;

var spdxSchemaString = File.ReadAllText("spdx.schema.json");
var cdxSchemaString = File.ReadAllText("bom-1.2.schema.json");
var jsonString = File.ReadAllText("valid-metadata-tool-1.2.json");

var spdxSchema = JsonSchema.FromText(spdxSchemaString);
SchemaRegistry.Global.Register(new Uri("file://spdx.schema.json"), spdxSchema);

var cdxSchema = JsonSchema.FromText(cdxSchemaString);

var validationOptions = new ValidationOptions
{
    OutputFormat = OutputFormat.Detailed,
    RequireFormatValidation = true
};

var jsonDocument = JsonDocument.Parse(jsonString);
var result = cdxSchema.Validate(jsonDocument.RootElement, validationOptions);
if (result.IsValid)
{
    // This is expected
    Console.WriteLine($"Valid JSON for you! {result.Message}");
}
else
{
    // This is not expected
    Console.WriteLine("NO VALID JSON FOR YOU!!!");
}

using System;
using System.Reflection;

namespace Hedin.UI.Demo.Shared.ApiDocs
{
    public class ApiProperty
    {
        public string Name { get; set; } = "";
        public Type? Type { get; set; }
        public PropertyInfo? PropertyInfo { get; set; }
        public string Description { get; set; } = "";
        public object? Default { get; set; }
        public bool IsTwoWay { get; set; }
    }
}

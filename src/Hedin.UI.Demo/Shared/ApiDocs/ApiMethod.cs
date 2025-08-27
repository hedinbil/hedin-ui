using System.Reflection;


namespace Hedin.UI.Demo.Shared.ApiDocs
{
    public class ApiMethod
    {
        public required string Signature { get; set; }
        public required ParameterInfo Return { get; set; }
        public required string Documentation { get; set; }
        public required MethodInfo MethodInfo { get; set; }
        public required ParameterInfo[] Parameters { get; set; }
    }
}

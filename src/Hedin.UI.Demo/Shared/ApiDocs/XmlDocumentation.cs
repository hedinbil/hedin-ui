// Borrowed from https://github.com/ZacharyPatten/Towel
using System.Reflection;
using System.Text.RegularExpressions;

namespace Hedin.UI.Demo.Shared.ApiDocs
{
    public static class XmlDocumentation
    {
        #region System.Reflection.Assembly

        /// <summary>Enumerates through all the properties with a custom attribute.</summary>
        /// <typeparam name="AttributeType">The type of the custom attribute.</typeparam>
        /// <param name="assembly">The assembly to iterate through the properties of.</param>
        /// <returns>The IEnumerable of the properties with the provided attribute type.</returns>
        public static IEnumerable<PropertyInfo> GetPropertyInfosWithAttribute<AttributeType>(this Type type)
            where AttributeType : Attribute
        {
            foreach (var propertyInfo in type.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic))
            {
                if (propertyInfo.GetCustomAttributes(typeof(AttributeType), true).Length > 0)
                {
                    yield return propertyInfo;
                }
            }
        }

        #endregion

        #region System.Type.ConvertToCSharpSource

        /// <summary>Converts a <see cref="Type"/> into a <see cref="string"/> as it would appear in C# source code.</summary>
        /// <param name="type">The <see cref="Type"/> to convert to a <see cref="string"/>.</param>
        /// <param name="showGenericParameters">If the generic parameters are the generic types, whether they should be shown or not.</param>
        /// <returns>The <see cref="string"/> as the <see cref="Type"/> would appear in C# source code.</returns>
        public static string ConvertToCSharpSource(this Type type, bool showGenericParameters = false)
        {
            var genericParameters = new Queue<Type>();
            foreach (var x in type.GetGenericArguments())
                genericParameters.Enqueue(x);
            return ConvertToCsharpSource(type);

            string ConvertToCsharpSource(Type type)
            {
                _ = type ?? throw new ArgumentNullException(nameof(type));
                var result = type.IsNested
                    ? ConvertToCsharpSource(type.DeclaringType!) + "."
                    : ""; //: type.Namespace + ".";
                result += Regex.Replace(type.Name, "`.*", string.Empty);
                if (type.IsGenericType)
                {
                    result += "<";
                    var firstIteration = true;
                    foreach (var generic in type.GetGenericArguments())
                    {
                        if (genericParameters.Count <= 0)
                        {
                            break;
                        }
                        var correctGeneric = genericParameters.Dequeue();
                        result += (firstIteration ? string.Empty : ",") +
                                  (correctGeneric.IsGenericParameter
                                      ? showGenericParameters ? (firstIteration ? string.Empty : " ") + correctGeneric.Name : string.Empty
                                      : (firstIteration ? string.Empty : " ") + correctGeneric.ConvertToCSharpSource());
                        firstIteration = false;
                    }
                    result += ">";
                }
                return result;
            }
        }
        #endregion
    }
}

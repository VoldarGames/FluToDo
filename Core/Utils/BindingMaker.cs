using System.Linq;

namespace Core.Utils
{
    public static class BindingMaker
    {
        public static string Make(params string[] bindingProperties)
        {
            var result = bindingProperties.Aggregate(string.Empty, (current, bindingProperty) => current + $"{bindingProperty}.");
            return result.TrimEnd('.');
        }
    }
}
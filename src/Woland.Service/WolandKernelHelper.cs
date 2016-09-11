namespace Woland.Service
{
    using System.Linq;
    using Ninject.Syntax;

    public static class WolandKernelHelper
    {
        public static IBindingNamedWithOrOnSyntax<T> InOncePerCallScope<T>(this IBindingWhenInNamedWithOrOnSyntax<T> syntax)
        {
            return syntax.InScope(x => x.Parameters.OfType<WolandKernel.OncePerCallParameter>().SingleOrDefault()?.Scope);
        }
    }
}
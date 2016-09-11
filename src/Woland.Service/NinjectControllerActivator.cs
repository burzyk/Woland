namespace Woland.Service
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Ninject;

    public class NinjectControllerActivator : IControllerActivator
    {
        private readonly IReadOnlyKernel kernel;

        public NinjectControllerActivator(IReadOnlyKernel kernel)
        {
            this.kernel = kernel;
        }

        public object Create(ControllerContext context)
        {
            return this.kernel.Get(
                context.ActionDescriptor.ControllerTypeInfo.AsType(),
                new WolandKernel.OncePerCallParameter());
        }

        public void Release(ControllerContext context, object controller)
        {
            this.kernel.Release(controller);
        }
    }
}
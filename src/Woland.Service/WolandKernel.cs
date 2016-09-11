namespace Woland.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ninject;
    using Ninject.Activation;
    using Ninject.Infrastructure.Disposal;
    using Ninject.Modules;
    using Ninject.Parameters;
    using Ninject.Planning.Targets;

    public class WolandKernel : StandardKernel
    {
        private readonly IDictionary<object, OncePerCallScope> scopes = new Dictionary<object, OncePerCallScope>();

        public WolandKernel(params INinjectModule[] modules)
            : base(modules)
        {
        }

        public override IEnumerable<object> Resolve(IRequest request)
        {
            var oncePerCall = request.Parameters.OfType<OncePerCallParameter>().SingleOrDefault();
            var result = base.Resolve(request).ToList();

            if (oncePerCall == null)
            {
                return result;
            }

            lock (this.scopes)
            {
                foreach (var implementation in result)
                {
                    this.scopes.Add(implementation, oncePerCall.Scope);
                }
            }

            return result;
        }

        public override bool Release(object instance)
        {
            var result = base.Release(instance);

            lock (this.scopes)
            {
                if (this.scopes.ContainsKey(instance))
                {
                    this.scopes[instance].Dispose();
                    this.scopes.Remove(instance);
                }
            }

            return result;
        }

        public class OncePerCallParameter : IParameter
        {
            public string Name => "Woland:OncePerCall";

            public bool ShouldInherit => true;

            public OncePerCallScope Scope { get; } = new OncePerCallScope();

            public bool Equals(IParameter other)
            {
                return this.Name == other.Name;
            }

            public object GetValue(IContext context, ITarget target)
            {
                return this.Scope;
            }
        }

        public class OncePerCallScope : INotifyWhenDisposed
        {
            public bool IsDisposed { get; set; }

            public event EventHandler Disposed;

            public void Dispose()
            {
                this.IsDisposed = true;
                this.Disposed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
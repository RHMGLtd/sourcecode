// Type: Castle.MicroKernel.Registration.LifestyleRegistrationExtensions
// Assembly: Castle.Windsor.Lifestyles, Version=0.1.0.2, Culture=neutral
// Assembly location: C:\business websites\lib\castle_windsor\Castle.Windsor.Lifestyles.dll

using Castle.MicroKernel.Registration.Lifestyle;

namespace Castle.MicroKernel.Registration
{
    public static class LifestyleRegistrationExtensions
    {
        public static ComponentRegistration<S> PerWebSession<S>(this LifestyleGroup<S> group);
        public static ComponentRegistration<S> PerHttpApplication<S>(this LifestyleGroup<S> group);
        public static ComponentRegistration<S> HybridPerWebRequestTransient<S>(this LifestyleGroup<S> group);
    }
}

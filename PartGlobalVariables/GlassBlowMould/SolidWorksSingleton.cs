using SolidWorks.Interop.sldworks;

namespace GlassBlowMould
{
    internal class SolidWorksSingleton
    {
        private static SldWorks? swApp;

        internal static SldWorks GetApplication()
        {
            if (swApp == null)
            {
                swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                swApp!.Visible = true;
                return swApp;
            }
            return swApp;
        }

        internal static void Dispose()
        {
            if (swApp != null)
            {
                swApp = null;
            }
        }
    }
}

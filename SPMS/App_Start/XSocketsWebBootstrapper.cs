using System.Web;
using XSockets.Core.Common.Socket;

[assembly: PreApplicationStartMethod(typeof(SPMS.App_Start.XSocketsWebBootstrapper), "Start")]

namespace SPMS.App_Start
{
    public static class XSocketsWebBootstrapper
    {
        private static IXSocketServerContainer wss;
        public static void Start()
        {
            wss = XSockets.Plugin.Framework.Composable.GetExport<IXSocketServerContainer>();
            wss.StartServers();
        }
    }
}

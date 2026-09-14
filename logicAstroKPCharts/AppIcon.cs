using System.Drawing;
using System.IO;
using System.Reflection;

namespace logicAstroKPCharts
{
    public static class AppIcon
    {
        private static Icon s_icon = null;

        public static Icon Logo
        {
            get
            {
                if (s_icon == null)
                {
                    using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("logicAstroKPCharts.App.ico"))
                    {
                        if (stream != null)
                        {
                            s_icon = new Icon(stream);
                        }
                    }
                }
                return s_icon;
            }
        }
    }
}
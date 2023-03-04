using Config.Net;

namespace IdleDisplayDimmer {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            IMySettings settings = new ConfigurationBuilder<IMySettings>()
               .UseYamlFile("settings.yaml")
               .Build();

            UserActivityMonitor uam = new UserActivityMonitor();

            int activeBrightnessLevel = 100;
            bool isInactive = false;
            while (true) {

                if (!isInactive && uam.InactivityPeriod.Seconds > settings.IdleTimeoutSeconds) {
                    isInactive = true;
                    activeBrightnessLevel = WmiFunctions.GetBrightnessLevel();
                    WmiFunctions.SetBrightnessLevel(settings.DimmedBrightnessLevel);
                }
                else if (isInactive && uam.InactivityPeriod.Seconds < 1) {
                    isInactive = false;
                    WmiFunctions.SetBrightnessLevel(activeBrightnessLevel);
                }

                Thread.Sleep(100);
            }
        }
    }
}
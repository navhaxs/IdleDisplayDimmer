using Config.Net;

namespace IdleDisplayDimmer {

    public interface IMySettings {
        [Option(DefaultValue = 0)]
        int DimmedBrightnessLevel { get; set; }

        [Option(DefaultValue = 120)]
        int IdleTimeoutSeconds { get; set; }
    }
}

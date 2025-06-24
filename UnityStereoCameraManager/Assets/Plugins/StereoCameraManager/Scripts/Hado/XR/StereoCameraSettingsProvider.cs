using DeviceModels;
using Hado.XR;
using UnityEngine.Device;

namespace TypeC.URP.URPStereoCameraManager.Scripts
{
    /// <summary>
    /// デバイスに応じたステレオカメラ設定を提供する
    /// </summary>
    public static class StereoCameraSettingsProvider
    {
        public static StereoCameraSettings Get() => Get(SystemInfo.deviceModel);

        public static StereoCameraSettings Get(string deviceModel)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (deviceModel == null) return StereoCameraSettings.Default;

            return iPhoneDeviceGenerations.Parse(deviceModel) switch
            {
                iPhoneDeviceGeneration.iPhone16e => StereoCameraSettings.iPhone16e,
                _ => StereoCameraSettings.Default
            };
        }
    }
}

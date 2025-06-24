namespace Hado.XR
{
    /// <summary>
    /// ステレオカメラの設定パラメータ
    /// </summary>
    public readonly struct StereoCameraSettings
    {
        public readonly float IpdMilli;
        public readonly float MagScale;
        public readonly float CenterY;

        public StereoCameraSettings(float ipdMilli, float magScale, float centerY)
        {
            IpdMilli = ipdMilli;
            MagScale = magScale;
            CenterY = centerY;
        }

        /// <summary>
        /// デフォルト設定（8/SE2/SE3）
        /// </summary>
        public static readonly StereoCameraSettings Default = new StereoCameraSettings(55f, 0.61f, 0.5f);

        /// <summary>
        /// iPhone 16e用設定
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static readonly StereoCameraSettings iPhone16e = new StereoCameraSettings(78.5f, 0.515f, 0.5f);
    }
}

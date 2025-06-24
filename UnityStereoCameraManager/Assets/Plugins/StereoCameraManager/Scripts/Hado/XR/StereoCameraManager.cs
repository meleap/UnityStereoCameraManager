using TypeC.URP.URPStereoCameraManager.Scripts;
using UnityEngine;

namespace Hado.XR
{
    // 単眼カメラの映像を左右に分割してステレオ表示するクラス
    [RequireComponent(typeof(Camera))]
    public class StereoCameraManager : MonoBehaviour
    {
        private static readonly int LeftCenterX = Shader.PropertyToID("_LeftCenterX");
        private static readonly int RightCenterX = Shader.PropertyToID("_RightCenterX");
        private static readonly int CenterY = Shader.PropertyToID("_CenterY");
        private static readonly int MagScale = Shader.PropertyToID("_MagScale");

        private Material _mat; // OnRender Imageで使用する単眼画像をステレオ描画するマテリアル

        private void Awake()
        {
            Shader shader = Shader.Find("XR/StereoShader");
            _mat = new Material(shader);
        }

        void Start()
        {
            // シェーダのパラメータを更新
            UpdateStatus();
        }

        // ステレオ表示パラメータに合わせてシェーダのパラメータを更新する
        public void UpdateStatus()
        {
            var settings = StereoCameraSettingsProvider.Get();

            var screenWidthMilli = GetScreenWidthMilli();
            var halfIpdRatio = settings.IpdMilli * 0.5f / screenWidthMilli;
            var rightCenterX = 0.5f + halfIpdRatio;
            var leftCenterX = 0.5f - halfIpdRatio;
            _mat.SetFloat(LeftCenterX, leftCenterX);
            _mat.SetFloat(RightCenterX, rightCenterX);
            _mat.SetFloat(CenterY, settings.CenterY);
            _mat.SetFloat(MagScale, settings.MagScale);
        }

        // 描画した画像にXR/StereoShaderを適用する
        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, _mat);
        }
        
        private static float GetScreenWidthMilli()
        {
            // 326はiPhoneSEのPPIで、25.4は1インチあたりのmm数
            // 16eはPPIが460ですが、MasScaleも変更する必要がある関係上、ここで分岐せず、StereoCameraSettingsで値を調整しています
            return Screen.width / 326f * 25.4f;
        }

    }

}

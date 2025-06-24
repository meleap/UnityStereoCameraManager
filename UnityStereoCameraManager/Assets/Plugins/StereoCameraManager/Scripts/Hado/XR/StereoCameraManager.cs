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

        public float ipdMilli = 55; // 瞳孔間距離[mm]
        public float centerY = 0.5f; // 高さの中心位置[0 1]
        public float magScale = 0.585f; // 表示領域の拡大率

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
            var screenWidthMilli = GetScreenWidthMilli();
            var halfIpdRatio = ipdMilli * 0.5f / screenWidthMilli;
            var rightCenterX = 0.5f + halfIpdRatio;
            var leftCenterX = 0.5f - halfIpdRatio;
            _mat.SetFloat(LeftCenterX, leftCenterX);
            _mat.SetFloat(RightCenterX, rightCenterX);
            _mat.SetFloat(CenterY, centerY);
            _mat.SetFloat(MagScale, magScale);
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

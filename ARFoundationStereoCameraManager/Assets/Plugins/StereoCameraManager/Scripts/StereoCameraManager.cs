using UnityEngine;

// 単眼カメラの映像を左右に分割してステレオ表示するクラス
[RequireComponent(typeof(Camera))]
public class StereoCameraManager : MonoBehaviour
{
    // ステレオ表示用パラメータ
    [Header("Stereo Camera")]
    [Tooltip("瞳孔間距離[mm]")]
    public float ipdMilli = 64; // 瞳孔間距離[mm]
    [Tooltip("高さの中心位置0~1")]
    public float centerY = 0.5f; // 高さの中心位置[0 1]
    [Tooltip("単眼カメラ映像の拡大率")]
    public float magScale = 0.8f; // 表示領域の拡大率

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
        float screenWidthMilli = PhysicalScreenInfo.GetScreenWidthMilli();
        float halfIpdRatio = ipdMilli * 0.5f / screenWidthMilli;
        float rightCenterX = 0.5f + halfIpdRatio;
        float leftCenterX = 0.5f - halfIpdRatio;
        _mat.SetFloat("_LeftCenterX", leftCenterX);
        _mat.SetFloat("_RightCenterX", rightCenterX);
        _mat.SetFloat("_CenterY", centerY);
        _mat.SetFloat("_MagScale", magScale);
    }

    // 描画した画像にXR/StereoShaderを適用する
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, _mat);
    }
}

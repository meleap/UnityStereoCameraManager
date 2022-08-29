using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Hado.XR
{
    public class StereoCameraRenderFeature : ScriptableRendererFeature
    {
        [SerializeField] private Shader stereoShader;
    
        // どのタイミングでパスを差し込むか
        [SerializeField] private RenderPassEvent renderPassEvent; 
    
        // ステレオ表示用パラメータ
        [Header("Stereo Camera")]
        [Tooltip("瞳孔間距離[mm]")]
        [SerializeField] private float ipdMilli = 55; // 瞳孔間距離[mm]
        [Tooltip("高さの中心位置0~1")]
        [SerializeField] private float centerY = 0.5f; // 高さの中心位置[0 1]
        [Tooltip("単眼カメラ映像の拡大率")]
        [SerializeField] private float magScale = 0.585f; // 表示領域の拡大率
        
        private StereoCameraRenderPass _renderPass;

        private static readonly int LeftCenterX = Shader.PropertyToID("_LeftCenterX");
        private static readonly int RightCenterX = Shader.PropertyToID("_RightCenterX");
        private static readonly int CenterY = Shader.PropertyToID("_CenterY");
        private static readonly int MagScale = Shader.PropertyToID("_MagScale");

        public override void Create()
        {
            var material = CoreUtils.CreateEngineMaterial(stereoShader);
            _renderPass = new StereoCameraRenderPass(material, renderPassEvent);
        
            var screenWidthMilli = PhysicalScreenInfo.GetScreenWidthMilli(); 
            var halfIpdRatio = ipdMilli * 0.5f / screenWidthMilli;
            var rightCenterX = 0.5f + halfIpdRatio;
            var leftCenterX = 0.5f - halfIpdRatio;
            
            material.SetFloat(LeftCenterX, leftCenterX);
            material.SetFloat(RightCenterX, rightCenterX);
            material.SetFloat(CenterY, centerY);
            material.SetFloat(MagScale, magScale);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            var uaCamera = renderingData.cameraData.camera.GetUniversalAdditionalCameraData();
            if (uaCamera.renderType == CameraRenderType.Base)
            {
                if (uaCamera.cameraStack.Count > 0) return;
            }
            _renderPass.SetParam(renderer.cameraColorTarget);
            renderer.EnqueuePass(_renderPass);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StereoCameeraRenderFeature : ScriptableRendererFeature
{
    [SerializeField] private Shader stereoShader;
    [SerializeField] private RenderPassEvent renderPassEvent;
    private StereoCameraRenderPass _renderPass;

    public override void Create()
    {
        var material = CoreUtils.CreateEngineMaterial(stereoShader);
        _renderPass = new StereoCameraRenderPass(material, renderPassEvent);
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

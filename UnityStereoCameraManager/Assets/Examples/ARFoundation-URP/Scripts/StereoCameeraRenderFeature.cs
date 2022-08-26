using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StereoCameeraRenderFeature : ScriptableRendererFeature
{

    private StereoCameraRenderPass _renderPass;

    public override void Create()
    {
        _renderPass = new StereoCameraRenderPass();
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        _renderPass.SetParam(renderer.cameraColorTarget);
        renderer.EnqueuePass(_renderPass);
    }
}

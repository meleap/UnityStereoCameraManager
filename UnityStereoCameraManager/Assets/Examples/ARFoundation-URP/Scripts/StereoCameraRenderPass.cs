using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StereoCameraRenderPass : ScriptableRenderPass
{
    private const string CommandBufferName = nameof(StereoCameraRenderPass);
    private readonly int RenderTargetTexId = Shader.PropertyToID("_RenderTargetTex");
    
    private RenderTargetIdentifier _currentRenderTarget;
    private readonly Material _material;
    
    private int _downSample = 10;
    
    public StereoCameraRenderPass(Material material, RenderPassEvent renderPassEvent)
    {
        _material = material;
        this.renderPassEvent = renderPassEvent;
    }
    
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        var commandBuffer = CommandBufferPool.Get(CommandBufferName);
        var cameraData = renderingData.cameraData;
        var w = cameraData.camera.scaledPixelWidth;
        var h = cameraData.camera.scaledPixelHeight;

        // RenderTextureを生成
        commandBuffer.GetTemporaryRT(RenderTargetTexId, w, h, 0, FilterMode.Bilinear);
        
        //ApplyShader
        commandBuffer.Blit(_currentRenderTarget, RenderTargetTexId, _material);
        
        // Back RenderTarget
        commandBuffer.Blit(RenderTargetTexId, _currentRenderTarget);
        commandBuffer.ReleaseTemporaryRT(RenderTargetTexId);

        context.ExecuteCommandBuffer(commandBuffer);
        context.Submit();
        
        CommandBufferPool.Release(commandBuffer);
    }

    public void SetParam(RenderTargetIdentifier renderTarget)
    {
        _currentRenderTarget = renderTarget;
    }
}

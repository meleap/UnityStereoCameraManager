using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StereoCameraRenderPass : ScriptableRenderPass
{
    private const string CommandBufferName = nameof(StereoCameraRenderPass);
    private const int RenderTextureId = 0;
    
    private RenderTargetIdentifier _currentTarget;
    
    private int _downSample = 10;
    
    public StereoCameraRenderPass()
    {
        renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }
    
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        var commandBuffer = CommandBufferPool.Get(CommandBufferName);
        var cameraData = renderingData.cameraData;
        
        var w = cameraData.camera.scaledPixelWidth / _downSample;
        var h = cameraData.camera.scaledPixelHeight / _downSample;

        // RenderTextureを生成
        commandBuffer.GetTemporaryRT(RenderTextureId, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
        
        // 現在のカメラ描画画像をRenderTextureにコピー
        commandBuffer.Blit(_currentTarget, RenderTextureId);
        
        // RenderTextureを現在のRenderTarget（カメラ）にコピー
        commandBuffer.Blit(RenderTextureId, _currentTarget);
        context.ExecuteCommandBuffer(commandBuffer);
        context.Submit();
        
        CommandBufferPool.Release(commandBuffer);
    }

    public void SetParam(RenderTargetIdentifier renderTarget)
    {
        _currentTarget = renderTarget;
    }
}

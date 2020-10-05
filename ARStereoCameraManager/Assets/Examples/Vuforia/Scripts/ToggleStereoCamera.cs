using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ToggleStereoCamera : MonoBehaviour
{
    public Button toggleURP;

    public RenderPipelineAsset DefaultPipelineAsset;
    public RenderPipelineAsset StereoCameraPipelineAsset;

    bool isStereo = false;

    void Start()
    {
        toggleURP.onClick.AddListener(Toggle);

    }

    void Toggle()
    {
        GraphicsSettings.renderPipelineAsset = isStereo ? DefaultPipelineAsset : StereoCameraPipelineAsset;
        isStereo = !isStereo;
    }
}

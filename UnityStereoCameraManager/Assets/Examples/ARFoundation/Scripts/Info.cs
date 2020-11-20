using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hado.XR.Examples
{
    public class Info : MonoBehaviour
    {
        [SerializeField] private Text ScreenWidth;

        [SerializeField] private Text DPI;

        [SerializeField] protected Text DeviceModel;
        
        private void Start()
        {
            DPI.text = $"DPI: {Screen.dpi:F4}";
            
            ScreenWidth.text = $"Screen: {Screen.width}px, {PhysicalScreenInfo.GetScreenWidthMilli()}milli";

            DeviceModel.text = $"Model: {SystemInfo.deviceModel}";
        }

        private void Update()
        {
            
        }
    }
}
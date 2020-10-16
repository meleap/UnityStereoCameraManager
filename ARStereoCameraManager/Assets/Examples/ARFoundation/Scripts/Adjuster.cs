using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hado.XR;

namespace Hado.XR.Examples
{
    public class Adjuster : MonoBehaviour
    {
        [SerializeField]
        StereoCameraManager _stereoCameraManager;

        [SerializeField]
        InputField _ipd;

        [SerializeField]
        InputField _magScale;

        [SerializeField]
        Button _submitBtn;

        void Start()
        {
            _submitBtn.onClick.AddListener(OnSubmit);
        }

        void OnSubmit()
        {
            if (_ipd.text != "")
            {
                _stereoCameraManager.ipdMilli = float.Parse(_ipd.text);
            }

            if (_magScale.text != "")
            {
                _stereoCameraManager.magScale = float.Parse(_magScale.text);
            }

            _stereoCameraManager.UpdateStatus();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Hado.XR.Examples
{
    [RequireComponent(typeof(ARCameraManager))]
    public class AutoFocusManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SetAufoFocusFixed());
        }

        // バッドノウハウ
        // ARCameraManagerのAutoFocus:Fixedがちゃんと動いてないので、ハック
        // ARCameraManager側のFocusModeはAutoに設定しておくこと
        IEnumerator SetAufoFocusFixed()
        {
            // 1フレームでも,0.1fでもだめ
            yield return new WaitForSeconds(3);
            GetComponent<ARCameraManager>().focusMode = CameraFocusMode.Fixed;
            yield return null;
        }
    }
}

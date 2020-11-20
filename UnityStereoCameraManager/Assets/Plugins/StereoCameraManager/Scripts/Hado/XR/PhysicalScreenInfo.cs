using UnityEngine;

namespace Hado.XR
{
    // 物理的なスクリーン情報を取得するクラス
    public class PhysicalScreenInfo : MonoBehaviour
    {
        // ピクセル単位からmm単位に変換する
        public static float PixToMilliMeter(float pix)
        {
            return pix / DPI * 25.4f;
        }

        // スクリーン幅をmm単位で取得する
        public static float GetScreenWidthMilli()
        {
            return PixToMilliMeter(Screen.width);
        }

        // スクリーンの高さをmm単位で取得する
        public static float GetScreenHeightMilli()
        {
            return PixToMilliMeter(Screen.height);
        }

        public static float DPI
        {
            get
            {
                if (SystemInfo.deviceModel == "iPhone13,3")
                {
                    return 460;
                }
                else
                {
                    return 326;
                }
            }
        }
    }
}
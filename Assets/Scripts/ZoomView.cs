using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ZoomView : MonoBehaviour
{
    [SerializeField] RenderTexture rt;

    public void Start()
    {
        StageManager.Instance.OnStageClear += () =>
        {
            SaveToPNG();
        };
    }
    
    public void SaveToPNG(string filePath = "./screenshot/")
    {
        // 1) 현재 Active RT 백업
        var prev = RenderTexture.active;
        // 2) 대상 RT 활성화
        RenderTexture.active = rt;

        // 3) 픽셀 복사할 Texture2D 생성 (rt 크기와 같은 크기로)
        var tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        // 화면 좌하단(0,0)부터 rt.width×rt.height 영역 읽기
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply();

        // 4) Encode to PNG (또는 EncodeToJPG)
        var bytes = tex.EncodeToPNG();

        var s = Guid.NewGuid().ToString()[..8];
        
        File.WriteAllBytes($"{filePath}/{s}.png", bytes);
        print($"Saved RenderTexture to: {filePath}");

        // 6) 복원 및 메모리 정리
        RenderTexture.active = prev;
        DestroyImmediate(tex);
    }
}
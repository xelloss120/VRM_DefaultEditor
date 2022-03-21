using UnityEngine;
using UnityEngine.UI;

public class Thumbnail : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] RawImage RawImage;

    /// <summary>
    /// プレビューの表示をサムネイルに適用
    /// </summary>
    public void OnClick()
    {
        var cam = Camera.targetTexture;
        var tex = new Texture2D(cam.width, cam.height, TextureFormat.RGB24, false);

        RenderTexture.active = cam;
        tex.ReadPixels(new Rect(0, 0, cam.width, cam.height), 0, 0);
        var color = tex.GetPixels();
        for (int i = 0; i < color.Length; i++)
        {
            color[i].r = Mathf.LinearToGammaSpace(color[i].r);
            color[i].g = Mathf.LinearToGammaSpace(color[i].g);
            color[i].b = Mathf.LinearToGammaSpace(color[i].b);
        }
        tex.SetPixels(color);
        tex.Apply();

        RawImage.texture = tex;
    }
}

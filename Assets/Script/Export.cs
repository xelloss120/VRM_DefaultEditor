using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using VRM;
using UniGLTF;
using VRMShaders;

public class Export : MonoBehaviour
{
    [SerializeField] RawImage Thumbnail;
    [SerializeField] InputField Title;
    [SerializeField] InputField Version;
    [SerializeField] InputField Author;
    [SerializeField] InputField Contact;
    [SerializeField] InputField Reference;

    public GameObject Model = null;

    /// <summary>
    /// VRMファイル書き出し
    /// </summary>
    public void OnClick()
    {
        var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "vrm");

        if (path == "") return;

        // 書き出しエラー対策（非表示にしたメッシュがあると書き出しエラー）
        var list = new List<VRMFirstPerson.RendererFirstPersonFlags>();
        foreach (var item in Model.GetComponent<VRMFirstPerson>().Renderers)
        {
            list.Add(item);
        }
        foreach (var item in list)
        {
            Model.GetComponent<VRMFirstPerson>().Renderers.Remove(item);
        }

        var meta = Model.GetComponent<VRMMeta>();
        meta.Meta.Thumbnail = ToTexture2D(Thumbnail.texture);
        meta.Meta.Title = Title.text;
        meta.Meta.Version = Version.text;
        meta.Meta.Author = Author.text;
        meta.Meta.ContactInformation = Contact.text;
        meta.Meta.Reference = Reference.text;

        var normalized = VRMBoneNormalizer.Execute(Model, false);
        var vrm = VRMExporter.Export(new GltfExportSettings(), normalized, new RuntimeTextureSerializer());
        var bytes = vrm.ToGlbBytes();
        File.WriteAllBytes(path, bytes);

        Destroy(normalized);
    }

    /// <summary>
    /// TextureからTexture2Dに変換
    /// </summary>
    /// <param name="tex"></param>
    /// <returns>Texture2D</returns>
    /// <remarks>https://baba-s.hatenablog.com/entry/2018/02/26/210100</remarks>
    public Texture2D ToTexture2D(Texture tex)
    {
        var result = new Texture2D(tex.width, tex.height);
        var currentRT = RenderTexture.active;
        var rt = new RenderTexture(tex.width, tex.height, 32);
        Graphics.Blit(tex, rt);
        RenderTexture.active = rt;
        var source = new Rect(0, 0, rt.width, rt.height);
        result.ReadPixels(source, 0, 0);
        result.Apply();
        RenderTexture.active = currentRT;
        return result;
    }
}

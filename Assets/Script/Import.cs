using UnityEngine;
using UnityEngine.UI;
using SFB;
using VRM;
using UniGLTF;

public class Import : MonoBehaviour
{
    [SerializeField] RawImage Thumbnail;
    [SerializeField] InputField Title;
    [SerializeField] InputField Version;
    [SerializeField] InputField Author;
    [SerializeField] InputField Contact;
    [SerializeField] InputField Reference;
    [SerializeField] DefaultEdit DefaultEdit;
    [SerializeField] Export Export;

    /// <summary>
    /// VRMファイル読み込み
    /// </summary>
    public void OnClick()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);

        if (paths.Length == 0) return;

        using (GltfData data = new AutoGltfFileParser(paths[0]).Parse())
        {
            var vrm = new VRMData(data);
            IMaterialDescriptorGenerator materialGen = default;
            using (var loader = new VRMImporterContext(vrm, materialGenerator: materialGen))
            {
                var instance = loader.Load();
                instance.ShowMeshes();

                var meta = instance.Root.GetComponent<VRMMeta>();
                Thumbnail.texture = meta.Meta.Thumbnail;
                Title.text = meta.Meta.Title;
                Version.text = meta.Meta.Version;
                Author.text = meta.Meta.Author;
                Contact.text = meta.Meta.ContactInformation;
                Reference.text = meta.Meta.Reference;

                // 書き出し時に重力設定で変形する対策
                var sb = instance.Root.GetComponentsInChildren<VRMSpringBone>();
                for (int i = 0; i < sb.Length; i++)
                {
                    sb[i].enabled = false;
                }

                if (Export.Model != null)
                {
                    Destroy(Export.Model);
                }
                Export.Model = instance.Root;

                DefaultEdit.Init(instance.Root);
            }
        }
    }
}

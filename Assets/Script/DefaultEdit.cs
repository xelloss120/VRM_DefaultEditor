using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultEdit : MonoBehaviour
{
    [SerializeField] GameObject BlendShapeCtrl;
    [SerializeField] GameObject BlendShapeCtrlContent;

    [SerializeField] GameObject MeshCtrl;
    [SerializeField] GameObject MeshCtrlContent;

    public void Init(GameObject model)
    {
        var meshs = new List<GameObject>();

        // ブレンドシェイプ調整
        foreach (Transform t in BlendShapeCtrlContent.transform)
        {
            Destroy(t.gameObject);
        }
        foreach (var mesh in model.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            meshs.Add(mesh.gameObject);
            for (int i = 0; i < mesh.sharedMesh.blendShapeCount; i++)
            {
                var item = Instantiate(BlendShapeCtrl);
                item.transform.parent = BlendShapeCtrlContent.transform;

                var ctrl = item.GetComponent<BlendShapeCtrl>();
                ctrl.Text.text = " " + mesh.sharedMesh.GetBlendShapeName(i);
                ctrl.Mesh = mesh;
                ctrl.Index = i;
            }
        }

        // 表示切り替え
        foreach (Transform t in MeshCtrlContent.transform)
        {
            Destroy(t.gameObject);
        }
        foreach (var mesh in model.GetComponentsInChildren<MeshRenderer>())
        {
            meshs.Add(mesh.gameObject);
        }
        foreach (var go in meshs)
        {
            var item = Instantiate(MeshCtrl);
            item.transform.parent = MeshCtrlContent.transform;

            var ctrl = item.GetComponent<MeshCtrl>();
            ctrl.Text.text = " " + go.name;
            ctrl.Mesh = go;
        }
    }
}

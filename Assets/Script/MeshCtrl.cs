using UnityEngine;
using UnityEngine.UI;

public class MeshCtrl : MonoBehaviour
{
    [SerializeField] Toggle Toggle;

    public Text Text;
    public GameObject Mesh;

    public void Changed()
    {
        Mesh.SetActive(Toggle.isOn);
    }
}

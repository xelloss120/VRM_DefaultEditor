using UnityEngine;
using UnityEngine.UI;

public class BlendShapeCtrl : MonoBehaviour
{
    [SerializeField] Slider Slider;
    [SerializeField] InputField InputField;

    public Text Text;
    public SkinnedMeshRenderer Mesh;
    public int Index;

    void Start()
    {
        InputField.text = Mesh.GetBlendShapeWeight(Index).ToString();
    }

    public void ChangedInputField()
    {
        Slider.value = float.Parse(InputField.text);
    }

    public void ChangedSlider()
    {
        InputField.text = Slider.value.ToString();
        Mesh.SetBlendShapeWeight(Index, Slider.value);
    }
}

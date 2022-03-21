using UnityEngine;
using UnityEngine.UI;

public class DefaultEdit : MonoBehaviour
{
    [SerializeField] InputField RootX;
    [SerializeField] InputField RootY;
    [SerializeField] InputField RootZ;

    [SerializeField] InputField HeadX;
    [SerializeField] InputField HeadY;
    [SerializeField] InputField HeadZ;

    [SerializeField] InputField ArmX;
    [SerializeField] InputField ArmY;
    [SerializeField] InputField ArmZ;

    [SerializeField] InputField LegX;
    [SerializeField] InputField LegY;
    [SerializeField] InputField LegZ;

    [SerializeField] InputField HipsY;

    Transform Root;
    Transform Head;
    Transform UpperArmL;
    Transform UpperArmR;
    Transform UpperLegL;
    Transform UpperLegR;
    Transform Hips;

    bool InitComp = false;

    public void Init(GameObject model)
    {
        Root = model.transform;

        var anim = model.GetComponent<Animator>();

        Head = anim.GetBoneTransform(HumanBodyBones.Head);
        UpperArmL = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        UpperArmR = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);
        UpperLegL = anim.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
        UpperLegR = anim.GetBoneTransform(HumanBodyBones.RightUpperLeg);

        Hips = anim.GetBoneTransform(HumanBodyBones.Hips);

        UpdateInputField();
    }

    public void OnValueChanged()
    {
        if (!InitComp) return;

        Root.localScale = TextToVec3(RootX.text, RootY.text, RootZ.text);
        Head.localScale = TextToVec3(HeadX.text, HeadY.text, HeadZ.text);
        UpperArmL.localScale = TextToVec3(ArmX.text, ArmY.text, ArmZ.text);
        UpperArmR.localScale = TextToVec3(ArmX.text, ArmY.text, ArmZ.text);
        UpperLegL.localScale = TextToVec3(LegX.text, LegY.text, LegZ.text);
        UpperLegR.localScale = TextToVec3(LegX.text, LegY.text, LegZ.text);

        Hips.localPosition = TextToVec3("0", HipsY.text, "0");
    }

    Vector3 TextToVec3(string textX, string textY, string textZ)
    {
        var x = float.Parse(textX);
        var y = float.Parse(textY);
        var z = float.Parse(textZ);

        return new Vector3(x, y, z);
    }

    void UpdateInputField()
    {
        InitComp = false;

        UpdateInputField(RootX, RootY, RootZ, Root);
        UpdateInputField(HeadX, HeadY, HeadZ, Head);
        UpdateInputField(ArmX, ArmY, ArmZ, UpperArmL);
        UpdateInputField(LegX, LegY, LegZ, UpperLegL);

        HipsY.text = Hips.localPosition.y.ToString();

        InitComp = true;
    }

    void UpdateInputField(InputField x, InputField y, InputField z, Transform transform)
    {
        x.text = transform.localScale.x.ToString();
        y.text = transform.localScale.y.ToString();
        z.text = transform.localScale.z.ToString();
    }
}

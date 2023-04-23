using UnityEngine;
using TMPro;

public class DefeatInfo : MonoBehaviour
{
    Animator Anim;
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] BasicSignal Listening;
    [SerializeField] string AddSignal;
    [SerializeField] string SubtractSignal;
    void Awake()
    {
        if(Listening) Listening.Signal += SignalBox;
    }
    void Start()
    {
        Anim = GetComponent<Animator>();
    }
    void SignalBox(string Message, GameObject Obj)
    {
        if(Message == AddSignal || Message == SubtractSignal)
        {
            PlayAnim();
            if(Message == AddSignal) {Text.text = "+1"; Text.color = Color.green;}
            if(Message == SubtractSignal) {Text.text = "-1"; Text.color = Color.red;}
        }
    }

    public void PlayAnim()
    {
        Anim.SetInteger("State", 1);
        Anim.Play("BlackHeartInfo", 0, 0);
    }

    public void StopAnim()
    {
        Anim.SetInteger("State", 0);
    }
}

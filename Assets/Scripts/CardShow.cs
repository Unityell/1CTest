using UnityEngine;

public class CardShow : BasicSignal
{
    Animator Anim;
    [SerializeField] BasicSignal[] Listening;
    [SerializeField] RectTransform CardParent;
    [SerializeField] string StartSignal;
    [SerializeField] string StopSignal;
    int StateInIerarchy;
    void Awake()
    {
        StateInIerarchy = transform.GetSiblingIndex();
        Anim = GetComponent<Animator>();
        for (int i = 0; i < Listening.Length; i++)
        {
            if(Listening[i] != null) Listening[i].Signal += SignalBox;
        }
    }
    public void SignalBox(string Message, GameObject Obj)
    {
        if(Message == StartSignal)
        {
            Anim.enabled = true;
            Anim.SetInteger("State", 0);
            Anim.Play("VFX", 0, 0);
        }
        if(Message == StopSignal)
        {
            Anim.enabled = true;
            Anim.SetInteger("State", 1);
            Anim.Play("Rotate", 0, 0);
        }
    }
    public void StopAnimation()
    {
        EmitSignal("CardReady", gameObject);
        Anim.enabled = false;
    }
    public void StopRotationAnim()
    {
        Anim.enabled = false;
        EmitSignal("CardStopRotation", gameObject);
        CardParent.anchoredPosition = new Vector3(0, -50, 0);
        transform.SetParent(transform);
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -50, 0);
        transform.SetSiblingIndex(StateInIerarchy);
    }
}

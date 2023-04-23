using UnityEngine;

public class MagicCircleFX : BasicSignal
{
    Animator Anim;
    [SerializeField] ParticleSystem Part;
    [SerializeField] BasicSignal Listening;
    [SerializeField] string TrackedSignalName;
    void Awake()
    {
        Anim = GetComponent<Animator>();
        if(Listening != null) Listening.Signal += SignalBox;
    }
    void SignalBox(string Message, GameObject Obj)
    {
        if(Message == TrackedSignalName)
        {
            Part.Play();
            Anim.enabled = true;
            Anim.SetInteger("State", 1);
            Anim.Play("VFX", 0, 0);
        }
    }
    public void StopAnimation()
    {
        Anim.SetInteger("State", 0);
        EmitSignal("MagicCircleStop", gameObject);
        Anim.enabled = false;
    }
}

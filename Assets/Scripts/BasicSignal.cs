using UnityEngine;

public abstract class BasicSignal : MonoBehaviour
{
    public delegate void MySignal(string Message, GameObject Obj);
    public event MySignal Signal;
    public void EmitSignal(string Message, GameObject Obj)
    {
        Signal?.Invoke(Message, Obj);
    }
}
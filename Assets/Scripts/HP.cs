using UnityEngine;
using System.Collections.Generic;

public class HP : BasicSignal
{
    [SerializeField] GameObject Prefab;
    public List<GameObject> Hearts;    
    [SerializeField] BasicSignal Listening;
    [SerializeField] string SubtractSignal;
    [SerializeField] string ResetSignal;
    int MaxHP;
    void Awake()
    {
        if(Listening) Listening.Signal += SignalBox;
    }
    public void Initialization(int MaxHP)
    {
        this.MaxHP = Mathf.Clamp(MaxHP, 1, 6);
        Reset();
    }
    void SignalBox(string Message, GameObject Obj)
    {
        if(Message == SubtractSignal) SubtractHP();
        if(Message == ResetSignal) Reset();
    }
    public void Reset()
    {
        if(Prefab)
        {
            for (int i = 0; i < Hearts.Count; i++)
            {
                Destroy(Hearts[i].gameObject);
            }
            Hearts.Clear();
            for (int i = 0; i < MaxHP; i++)
            {
                var NewHeart = Instantiate(Prefab, transform);
                Hearts.Add(NewHeart);
            }
        }
    }
    public void SubtractHP()
    {
        if(Hearts.Count > 0)
        {
            Destroy(Hearts[Hearts.Count - 1]);
            Hearts.Remove(Hearts[Hearts.Count - 1]);
        }
        if(Hearts.Count <= 0)
        {
            EmitSignal("Defeat", gameObject);
        }
    }
}

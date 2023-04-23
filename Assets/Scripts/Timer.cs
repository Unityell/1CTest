using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : BasicSignal
{
    float GameTimer;
    [SerializeField] RectTransform LineTimer;
    [SerializeField] TextMeshProUGUI NumericTimer;
    bool IsInit;
    float SaveSizeDelta;
    [SerializeField] BasicSignal Listening;
    [SerializeField] string StartSignal;
    [SerializeField] string StopSignal;
    void Awake()
    {
        if(Listening) Listening.Signal += SignalBox;
    }
    public void Initialization(float GameTimer)
    {
        SaveSizeDelta = LineTimer.sizeDelta.x;
        this.GameTimer = GameTimer;
    }
    public void SignalBox(string Message, GameObject Obj)
    {
        if(Message == StartSignal) 
        {
            StartCoroutine(CountDown());
        }
        if(Message == StopSignal)
        {
            StopAllCoroutines();
            NumericTimer.text = "";
        }
    }
    IEnumerator CountDown()
    {
        float TotalTime = GameTimer;
        while(TotalTime <= GameTimer)
        {
            TotalTime -= Time.deltaTime;
            var Procent = TotalTime/(GameTimer/100) / 100;
            LineTimer.sizeDelta = new Vector2(SaveSizeDelta * Procent, LineTimer.sizeDelta.y);
            var Integer = (int)TotalTime;
            NumericTimer.text = (1 + Integer).ToString();
            if(TotalTime <= 0)
            {
                NumericTimer.text = "";
                EmitSignal("TimerStop", gameObject);
                break; 
            }
            yield return null;
        }
    }
}

using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    int CurrentScore;
    Animator Anim;    
    [SerializeField] BasicSignal Listening;
    [SerializeField] string AddSignal;
    [SerializeField] string ResetSignal;
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
        if(Message == AddSignal) AddScore();
        if(Message == ResetSignal) Reset();
    }
    public int GetScore()
    {
        return CurrentScore;
    }
    public void AddScore()
    {
        CurrentScore++;
        ScoreText.text = CurrentScore.ToString();
        Anim.Play("Score", 0, 0);
    }

    public void Reset()
    {
        CurrentScore = 0;
        ScoreText.text = "0";
    }
}

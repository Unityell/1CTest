using UnityEngine;

public class Controller : BasicSignal
{
    [SerializeField] HUD HUD;
    [SerializeField] Config Config;
    [SerializeField] CardFactory CardFactory;
    
    void Start()
    {
        HUD.Timer.Initialization(Config.Timer);
        HUD.HP.Initialization(Config.HeartsCount);
        Subscribe();
        ResetGame();
    }
    void Subscribe()
    {
        HUD.CardShow.Signal += SignalBox;
        HUD.MagicCircle.Signal += SignalBox;
        HUD.Timer.Signal += SignalBox;
        CardFactory.Signal += SignalBox;
        HUD.DragController.Signal += SignalBox;
        HUD.HP.Signal += SignalBox;
    }
    public void ResetGame()
    {
        EmitSignal("Reset", gameObject);
        EmitSignal("GetCard", gameObject);
    }
    void SignalBox(string Message, GameObject Obj)
    {
        if(Message == "TimerStop" && !HUD.FinalPannel.gameObject.activeInHierarchy) {EmitSignal("SubtractHP", gameObject);  EmitSignal("StopShow", gameObject); EmitSignal("TimerStop", gameObject);}
        if(Message == "CardStopRotation" && !HUD.FinalPannel.gameObject.activeInHierarchy) {EmitSignal("GetCard", gameObject);}
        if(Message == "CardReady") {EmitSignal("On", gameObject); EmitSignal("TimerStart", gameObject);}
        if(Message == "AddScore") {EmitSignal("AddScore", gameObject); EmitSignal("StopShow", gameObject); EmitSignal("TimerStop", gameObject);}
        if(Message == "SubtractHP") {EmitSignal("SubtractHP", gameObject);  EmitSignal("StopShow", gameObject); EmitSignal("TimerStop", gameObject);}
        if(Message == "Defeat") {HUD.FinalPannel.gameObject.SetActive(true); HUD.FinalPannel.ShowResult(HUD.Score.GetScore()); EmitSignal("TimerStop", gameObject);} 
    }
}

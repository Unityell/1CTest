using UnityEngine;
using TMPro;

public class FinalPannel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Finalscore;
    Animator Anim;
    public void ShowResult(int Score)
    {
        Anim = GetComponent<Animator>();
        Anim.Play("ShowTablet", 0, 0);
        Finalscore.text = $"Счёт: {Score}";
    }
}

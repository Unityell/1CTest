using UnityEngine;

[CreateAssetMenu]
public class Config : ScriptableObject
{   
    [SerializeField] private float _Timer;
    public float Timer => this._Timer;
    [SerializeField] private int _HeartsCount;
    public int HeartsCount => this._HeartsCount;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class CardFactory : BasicSignal
{
    [SerializeField] List<Settings> Eatable;
    [SerializeField] List<Settings> Uneatable;
    [SerializeField] Image CardShow;
    AsyncOperationHandle operationHandle;
    [SerializeField] BasicSignal Listening;
    [SerializeField] string TrackedSignalName;
    [SerializeField] TextMeshProUGUI AssetsNameText;

    void Awake()
    {
        if(Listening) Listening.Signal += SignalBox;
    }
    void SignalBox(string Message, GameObject Obj)
    {
        if(Message == TrackedSignalName)
        {
            Caching.ClearCache();
            GetRandomCard();
        }
    }
    void GetRandomCard()
    {
        switch (Random.Range(0, 2))
        {
            case 0 :
                StartCoroutine(FindCard(Random.Range(0, Eatable.Count), Eatable));
                break;
            case 1 :
                StartCoroutine(FindCard(Random.Range(0, Uneatable.Count), Uneatable));
                break;
            default: break;
        }
    }
    void SetSpriteInSocket(Sprite Sprite)
    {
        CardShow.sprite = Sprite;
    }
    IEnumerator FindCard(int Number, List<Settings> List)
    {
        if(operationHandle.IsValid()) Addressables.Release(operationHandle);
        AssetReference Sprite = List[Number].Sprites;
        operationHandle = Sprite.LoadAssetAsync<Sprite>();
        yield return operationHandle;
        if(List == Eatable) CardShow.transform.parent.tag = "Eatable"; else CardShow.transform.parent.tag = "Uneatable";
        AssetsNameText.text = List[Number].Name;
        SetSpriteInSocket((Sprite)operationHandle.Result);
        EmitSignal("StartShow", gameObject);
    }
}
[System.Serializable]
public class Settings
{
    public string Name;
    public AssetReference Sprites;
}

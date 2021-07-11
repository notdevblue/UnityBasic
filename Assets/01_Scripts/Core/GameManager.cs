using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static Transform Player
    {
        get
        {
            return instance.player;
        }
    }
    public Transform player;
    public GameObject bloodParticlePrefab;

    public DialogPannel dialogPannel;
    private Dictionary<int, List<TextVO>> dialogTextDictionary = new Dictionary<int, List<TextVO>>();

    private float timeScale = 1.0f;
    public static float TimeScale
    {
        get
        {
            return instance.timeScale;
        }
        set
        {
            instance.timeScale = Mathf.Clamp(value, 0, 1);
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("<color=ffcccc>WARN</color>: There are more than one GameManager running in same scene");
        }

        instance = this;

        // 확장자 쓰면 안됨
        TextAsset dJson = Resources.Load("dialogText") as TextAsset;
        GameTextDataVO textData = JsonUtility.FromJson<GameTextDataVO>(dJson.ToString());

        foreach (DialogVO vo in textData.list)
        {
            dialogTextDictionary.Add(vo.code, vo.text);
        }
    }

    private void Start()
    {
        PoolManager.CreatePool<BloodParticle>(bloodParticlePrefab, transform, 10);

        
    }

    public static void ShowDialog(int index)
    {
        if (index >= instance.dialogTextDictionary.Count)
        {
            return;
        }

        instance.dialogPannel.StartDialog(instance.dialogTextDictionary[index]);
    }

        
}

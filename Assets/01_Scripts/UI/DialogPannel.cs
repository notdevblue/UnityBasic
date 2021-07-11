using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogPannel : MonoBehaviour
{
    private List<TextVO>  list;
    private RectTransform panel;

    public  TMP_Text dialogText;
    private WaitForSeconds shortWs = new WaitForSeconds(0.2f);

    private bool clickToNext = false;
    private bool isOpen      = false;

    public GameObject nextIcon;
    public GameObject typeEffectParticle;
    public Image      profileImage;
    public AudioClip  typeClip;

    private int curIdx;
    private RectTransform textTransform;

    private Dictionary<int, Sprite> imageDictionary = new Dictionary<int, Sprite>();

    private void Awake()
    {
        dialogText.text = "";
        panel = GetComponent<RectTransform>();
        textTransform = dialogText.GetComponent<RectTransform>();
    }

    public void StartDialog(List<TextVO> list)
    {
        this.list = list;
        ShowDialog();
    }

    public void ShowDialog()
    {
        curIdx = 0;

        panel.DOScale(new Vector3(1, 1, 1), 0.8f).OnComplete(() =>
           {
               GameManager.TimeScale = 0.0f;
               TypeIt(list[curIdx]);
               isOpen = true;
           });
    }

    public void TypeIt(TextVO vo)
    {
        int idx = vo.icon;

        if (!imageDictionary.ContainsKey(idx))
        {
            Sprite img = Resources.Load<Sprite>($"profile{idx}");
            imageDictionary.Add(idx, img);
        }

        profileImage.sprite = imageDictionary[idx];

        dialogText.text = vo.msg;
        nextIcon.SetActive(false);
        clickToNext = false;
        StartCoroutine(Typing());
    }


    private IEnumerator Typing()
    {
        dialogText.ForceMeshUpdate();
        dialogText.maxVisibleCharacters = 0;

        int totalVisibleChar = dialogText.textInfo.characterCount;
        for (int i = 1; i <= totalVisibleChar; ++i)
        {
            dialogText.maxVisibleCharacters = i;

            //Vector3 pos  = dialogText.textInfo.characterInfo[i - 1].bottomRight;
            //Vector3 tpos = textTransform.TransformPoint(pos); // <= »ó´ëÁÂÇ¥·Î º¯È¯µÊ

            //sound

            if (clickToNext)
            {
                dialogText.maxVisibleCharacters = totalVisibleChar;
                break;
            }
            yield return shortWs;
        }

        ++curIdx;
        clickToNext = true;
        nextIcon.SetActive(true);
    }

    private void Update()
    {
        if (!isOpen) return;

        if (Input.GetButtonDown("Jump") && clickToNext)
        {
            if (curIdx >= list.Count)
            {
                panel.DOScale(new Vector3(0, 0, 1), 0.8f).OnComplete(() => 
                {
                    GameManager.TimeScale = 1.0f;
                    isOpen = false;
                });
            }
            else
            {
                TypeIt(list[curIdx]);
            }
        }
        else if (Input.GetButtonDown("Jump"))
        {
            clickToNext = true;
        }
    }
}

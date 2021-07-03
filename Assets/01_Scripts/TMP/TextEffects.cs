using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffects : MonoBehaviour
{
    private TMP_Text tmpText;
    private Mesh mesh;
    private Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        StartCoroutine(TypeWriter(0.2f));
    }

    // Update is called once per frame
    void Update()
    {

        

        //mesh     = tmpText.mesh;  // 글자 메시 정보를 가져옴
        //vertices = mesh.vertices; // 글자들의 정점

        //for (int i = 0; i < tmpText.textInfo.characterCount; ++i)
        //{
        //    TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

        //    // 공백 또는 Enter (보이지 않는 캐릭터(char))는 처리하지 않고 continues 로 보낸다.
        //    if (!c.isVisible)
        //    {
        //        continue;
        //    }

        //    int idx = c.vertexIndex; // 이 캐릭터의 정점 순서 번호
        //    Vector3 offset = Vector3.zero;
            
        //    // 해당 캐릭터에는 4개의 정점이 있고 각 4개의 정점을 모두 동일한 값으로 넣는다
        //    for (int j = 0; j < 4; ++j)
        //    {
        //        //vertices[idx + j] += offset;
        //    }
        //}
    }

    private IEnumerator TypeWriter(float interval)
    {
        Debug.Log("asdasd123");
        WaitForSeconds wait = new WaitForSeconds(interval);
        string originText = tmpText.text;
        int txtCount = tmpText.textInfo.characterCount;
        Debug.Log(tmpText.text);
        Debug.Log(tmpText.textInfo.characterCount);
        Debug.Log(txtCount);
        tmpText.text = "";

        for (int i = 0; i < txtCount; ++i)
        {
            Debug.Log("asdasd");
            tmpText.text += originText[i];
            tmpText.ForceMeshUpdate();
            yield return wait;
        }
    }
}

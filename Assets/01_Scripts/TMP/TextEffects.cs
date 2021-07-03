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

        

        //mesh     = tmpText.mesh;  // ���� �޽� ������ ������
        //vertices = mesh.vertices; // ���ڵ��� ����

        //for (int i = 0; i < tmpText.textInfo.characterCount; ++i)
        //{
        //    TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

        //    // ���� �Ǵ� Enter (������ �ʴ� ĳ����(char))�� ó������ �ʰ� continues �� ������.
        //    if (!c.isVisible)
        //    {
        //        continue;
        //    }

        //    int idx = c.vertexIndex; // �� ĳ������ ���� ���� ��ȣ
        //    Vector3 offset = Vector3.zero;
            
        //    // �ش� ĳ���Ϳ��� 4���� ������ �ְ� �� 4���� ������ ��� ������ ������ �ִ´�
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

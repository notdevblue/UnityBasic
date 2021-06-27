using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{
    private TMP_Text  tmpText;
    private Mesh      mesh;
    private Vector3[] vertices;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    private void Update() // TODO : 숙제 = 이팩트
    {
        // 변경한 값들을 메시 업데이트를 통해 업데이트 시킨다.
        tmpText.ForceMeshUpdate();

        mesh = tmpText.mesh; // 글자 메시 정보를 가져옴
        vertices = mesh.vertices; // 글자들의 정점
        // 각 글자는 전부 4개의 정점으로 이루어져 있음


        //for (int i = 0; i < vertices.Length; ++i)
        //{
        //    Vector3 offset = Wobble(Time.time + i);
        //    vertices[i] += offset;
        //}

        for (int i = 0; i < tmpText.textInfo.characterCount; ++i)
        {
            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

            // 공백 또는 Enter (보이지 않는 캐릭터(char))는 처리하지 않고 continues 로 보낸다.
            if (!c.isVisible)
            {
                continue;
            }

            int idx = c.vertexIndex; // 이 캐릭터의 정점 순서 번호
            Vector3 offset = Wobble(Time.time + i);

            // 해당 캐릭터에는 4개의 정점이 있고 각 4개의 정점을 모두 동일한 값으로 넣는다
            for (int j = 0; j < 4; ++j)
            {
                vertices[idx + j] += offset;
            }
        }

        mesh.vertices = vertices;
        tmpText.canvasRenderer.SetMesh(mesh);
    }


    private Vector2 Wobble(float time)
    {
        float x = Mathf.Sin(time * 4.0f) * 1.5f; // 메개변수에 * 하면 주기가 빨라짐
        float y = Mathf.Cos(time * 2.2f) * 1.3f; // 밖에 * 하면 진폭이 빨라짐

        return new Vector2(x, y);
    }


    
    // 과제
    /*
    1. 우리가 어떤 게임에서 대화창에 사용할 예정
       택스트에 들어갈 이팩트를 만들어 와라.

    개쩔게 만들면 문상
    

    */
}

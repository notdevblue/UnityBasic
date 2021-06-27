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

    private void Update() // TODO : ���� = ����Ʈ
    {
        // ������ ������ �޽� ������Ʈ�� ���� ������Ʈ ��Ų��.
        tmpText.ForceMeshUpdate();

        mesh = tmpText.mesh; // ���� �޽� ������ ������
        vertices = mesh.vertices; // ���ڵ��� ����
        // �� ���ڴ� ���� 4���� �������� �̷���� ����


        //for (int i = 0; i < vertices.Length; ++i)
        //{
        //    Vector3 offset = Wobble(Time.time + i);
        //    vertices[i] += offset;
        //}

        for (int i = 0; i < tmpText.textInfo.characterCount; ++i)
        {
            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

            // ���� �Ǵ� Enter (������ �ʴ� ĳ����(char))�� ó������ �ʰ� continues �� ������.
            if (!c.isVisible)
            {
                continue;
            }

            int idx = c.vertexIndex; // �� ĳ������ ���� ���� ��ȣ
            Vector3 offset = Wobble(Time.time + i);

            // �ش� ĳ���Ϳ��� 4���� ������ �ְ� �� 4���� ������ ��� ������ ������ �ִ´�
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
        float x = Mathf.Sin(time * 4.0f) * 1.5f; // �ް������� * �ϸ� �ֱⰡ ������
        float y = Mathf.Cos(time * 2.2f) * 1.3f; // �ۿ� * �ϸ� ������ ������

        return new Vector2(x, y);
    }


    
    // ����
    /*
    1. �츮�� � ���ӿ��� ��ȭâ�� ����� ����
       �ý�Ʈ�� �� ����Ʈ�� ����� �Ͷ�.

    ��¿�� ����� ����
    

    */
}
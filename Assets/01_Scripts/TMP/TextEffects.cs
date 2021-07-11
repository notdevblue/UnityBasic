using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextEffects : MonoBehaviour
{
    #region WinAPI import
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    private static extern IntPtr FindWindow(string className, string windowName);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(HandleRef hwnd, out RECT lpRect);


    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
#endif
    #endregion

    IntPtr activeHwnd;
    RECT rc;

    protected void SetLocation(int xPos, int yPos, int xScale = 1280, int yScale = 720)
    {
        SetWindowPos(activeHwnd, 0, xPos, yPos, xScale, yScale, 1);
    }

    private Vector2Int mid;

    public TMP_Text tmpText;
    private Mesh mesh;
    private Vector3[] vertices;

    public delegate void OnComplete();

    public AudioSource sound = null;
    public GameObject pannel = null;
    public Camera main = null;



    // Start is called before the first frame update
    void Start()
    {
        activeHwnd = GetActiveWindow();
        MoveWindow(activeHwnd, mid.x, mid.y, 1280, 720, true);
        GetWindowRect(new HandleRef(this, activeHwnd), out rc);

        mid = new Vector2Int((1920 / 2 - (rc.Right - rc.Left) / 2), (1080 / 2 - (rc.Bottom - rc.Top) / 2));

        Screen.SetResolution(1280, 720, false);
        pannel.SetActive(false);
        tmpText.text = "";

        MoveWindow(activeHwnd, mid.x, mid.y, 1280, 720, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            sound.Play();
            pannel.SetActive(true);
            StartCoroutine(_Explosion(tmpText));
        }

    }
    
    private IEnumerator _Explosion(TMP_Text origin, OnComplete callback = null)
    {
        #region Old typewriter effect
        //WaitForSeconds wait = new WaitForSeconds(interval);
        //origin.text = "";

        //for (int i = 0; i < text.Length; ++i)
        //{
        //    origin.text += text[i];
        //    origin.ForceMeshUpdate();
        //    yield return wait;
        //}

        //callback?.Invoke();
        #endregion

        StartCoroutine(WindowEffects());

        main.DOColor(new Color32(250, 164, 126, 255), 8.7f);

        tmpText.text = "익";
        yield return new WaitForSeconds(0.284f);


        tmpText.text += "스";
        yield return new WaitForSeconds(0.515f - 0.284f);


        tmpText.text += "플";
        yield return new WaitForSeconds(0.642f - 0.515f);


        tmpText.text += "로";
        float time = Time.time;
        while (Time.time < time + (1.625f - 0.642f))
        {
            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[3];
            tmpText.ForceMeshUpdate();

            Vector3 offset = Wobble();

            mesh = tmpText.mesh;
            vertices = mesh.vertices;


            for (int j = 0; j < 4; ++j)
            {
                vertices[c.vertexIndex + j] += offset;
            }

            mesh.vertices = vertices;
            tmpText.canvasRenderer.SetMesh(mesh);
            yield return new WaitForEndOfFrame();
        }


        tmpText.text += "전!";
        
        yield return new WaitForSeconds(8.669f - 1.625f);
        
    }

    private Vector2 Wobble()
    {
        float x = Mathf.Sin(Time.time * 120.0f) * 4.0f;
        float y = Mathf.Cos(Time.time * 150.0f) * 7.0f;
        return new Vector2(x, y);
    }


    private IEnumerator WindowEffects()
    {
        float time = Time.time;
        float mul = 1.0f;

        while (Time.time < time + /*1.625f*/8.7f)
        {
            mul += 0.02f;
            if (mul > 20.0f)
                mul = 20.0f;

            int x = (int)((Mathf.Sin(Time.time * (mul / 5.0f)) * mul * 10.0f)/* * Time.deltaTime * 100.0f*/);
            int y = (int)((Mathf.Cos(Time.time * (mul / 5.0f)) * mul * 10.0f)/* * Time.deltaTime * 100.0f*/);
            SetLocation(mid.x + x, mid.y + y);
            yield return new WaitForEndOfFrame();
        }

        SetLocation(mid.x, mid.y);

        int dec = 0;
        main.backgroundColor = new Color(1, 0.2f, 0.2f);
        while (Time.time < time + 11.4f)
        {
            dec += 2;
            if (dec > 200)
                dec = 200;
            int x = UnityEngine.Random.Range(-8 + dec, 1920 - (rc.Right - rc.Left) - dec);
            int y = UnityEngine.Random.Range(-8 + dec, 1080 - (rc.Bottom - rc.Top) - dec);

            SetLocation(x, y);
            yield return new WaitForEndOfFrame();
        }

        SetLocation(mid.x, mid.y);
        //Application.ForceCrash(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private int index;

    private bool isRead = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isRead && collision.CompareTag("Player"))
        {
            isRead = true;
            GameManager.ShowDialog(index);
        }
    }
}

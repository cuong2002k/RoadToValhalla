using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Color color;
    private float moveY = 2f;
    private float destroyTimer = 1f;
    private CountDownTimer destrouCountDown;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        color = textMesh.color;
        destrouCountDown = new CountDownTimer(destroyTimer);
    }

    public void SetDamePopup(int damePopup)
    {
        textMesh.text = damePopup.ToString();
    }

    private void Update()
    {
        this.transform.position += new Vector3(0, moveY) * Time.deltaTime;
        destrouCountDown.Tick(Time.deltaTime);
        if (destrouCountDown.Timer >= 0)
        {
            float destroyspeed = 3f;
            color.a -= destroyspeed * Time.deltaTime;
            if (color.a <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

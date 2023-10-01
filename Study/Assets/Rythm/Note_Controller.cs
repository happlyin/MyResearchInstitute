using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Controller : MonoBehaviour
{
    public Notes_Spawner spwaner;
    public RectTransform mytransform;
    public bool IsDown = false;
    public float CheckPosY = 0;
    public float Timer = 5.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsDown && spwaner != null)
        {
            Timer -= Time.deltaTime;
            Vector2 nowPos = mytransform.anchoredPosition;
            mytransform.anchoredPosition = new Vector2(nowPos.x, CheckPosY + spwaner.NoteSpeed * Timer * spwaner.LengthPosY / spwaner.RunTime);

            if (mytransform.anchoredPosition.y <= CheckPosY)
                this.gameObject.SetActive(false);
        }
    }

    public void StartDown(float checkPoint, Notes_Spawner sp, float RunTime)
    {
        mytransform = this.GetComponent<RectTransform>();
        spwaner = sp;
        CheckPosY = checkPoint;
        Timer = RunTime;
        IsDown = true;
    }

    public float SlovePosY(Vector2 nowPos)
    {
        if (spwaner != null)
            return (nowPos.y - 200 ) / 9.0f * spwaner.NoteSpeed + 200;
        else
            return nowPos.y - 1.0f / 3.0f;
    }
}

/* Check Data
 * 1.0 :: 18.0064
 * 1.1 :: 16.364
 * 1.2 :: 15.004
 */
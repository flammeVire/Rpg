using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    public Pnj_Data[] ennemis = new Pnj_Data[3];

    public void GoToFight(Transform transform)
    {
        GameManager.instance.oldPlayerPosition = transform.position;
        GameManager.instance.oldPlayerRotation = transform.rotation;
        PrepareEnnemis();
        Debug.Log("aaa" + GameManager.instance.oldPlayerPosition);
        GameManager.instance.PrepareTransition(-1,1);
    }

    public void PrepareEnnemis()
    {
        for (int i = 0; i < ennemis.Length; i++) 
        {
            if(ennemis[i] != null)
            {
                Pnj_Data clone = Instantiate(ennemis[i]);
                GameManager.instance.nextEnnemis[i] = clone;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Set_Brick : ColorObject
{
    private void Start()
    {
        ChangeColor((ColorType)Random.Range(1, 9));
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player.colorType == colorType)
            {
                gameObject.SetActive(false);
                //other.GetComponent<Player>().AddBrick();
                Invoke(nameof(SpwamBrick), 2f);
            }
        }
        if (other.CompareTag("Bot"))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot.colorType == colorType)
            {
                gameObject.SetActive(false);
                Invoke(nameof(SpwamBrick), 2f);
            }
        }
    }

    
    

    private void SpwamBrick()
    {
        //ChangeColor((ColorType)Random.Range(1, 9));
        gameObject.SetActive(true);
    }
}

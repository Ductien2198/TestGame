using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Brick : ColorObject
{
    [SerializeField] private int gridX = 10;
    [SerializeField] private int gridZ = 10;
    [SerializeField] private float gridSpacingOffset = 2f;

    public List<Set_Brick> bricks = new List<Set_Brick>();
    //[HideInInspector] public Stage stage;

    private void Start()
    {
        LoadMap();
    }



    public void LoadMap()
    {
        Vector3 startPos = transform.position - new Vector3(gridX * .5f * gridSpacingOffset, 0, gridZ * .5f * gridSpacingOffset);

        for (int x = 0; x <= gridX; x++)
        {
            for (int z = 0; z <= gridZ; z++)
            {
                Vector3 spawnPos = startPos + new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset);


                Set_Brick brick = Instantiate(GameAssets.Instance.Brick, spawnPos, Quaternion.identity);
                

                bricks.Add(brick);

                Debug.Log(bricks.Count);
            }
        }
        
    }
    
    internal Set_Brick SeekBrickPoint(ColorType colorType)
    {
        Set_Brick brick = null;
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                brick = bricks[i];
                break;
            }
        }

        return brick;
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance;
    public Set_Brick Brick;

    private void Awake()
    {
        Instance = this;
    }
}

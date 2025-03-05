using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BuildValue
{
    public Sprite Sprite;
    public Vector2Int Size ;
    public Vector2 offset;
}

[CreateAssetMenu(fileName = "NewBuild", menuName = "Builds/BuildData")]



public class Builds : ScriptableObject
{
    public List<BuildValue> Values = new List<BuildValue>();
}
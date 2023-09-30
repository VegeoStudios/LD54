using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoxTypeAssets", menuName = "Custom/BoxTypeAssets")]
public class BoxTypeAssets : ScriptableObject
{
    public Texture2D[] textures;
    public Color[] colors;
}

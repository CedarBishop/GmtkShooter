using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Nerf Data Information")]
public class NerfData : ScriptableObject
{
    public Nerf nerf;
    public Sprite nerfIcon;
    public Color badgeColor;
}

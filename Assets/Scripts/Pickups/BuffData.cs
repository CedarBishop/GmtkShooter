using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff Data Information")]
public class BuffData : ScriptableObject
{
    public Buff buff;
    public Sprite buffIcon;
    public Color badgeColor;
}

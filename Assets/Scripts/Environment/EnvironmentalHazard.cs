using UnityEngine;
public enum HazardType {
    Vines,
    Lava
}
public class EnvironmentalHazard : MonoBehaviour
{
    public float damage = 1f;
    public HazardType hT;
}

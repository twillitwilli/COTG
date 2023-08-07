using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyPools/CreateEnemySkinList")]
public class EnemySkins : ScriptableObject
{
    public List<Material> batSkins;
    public List<Material> beeSkins;
    public List<Material> bunnySkins;
    public List<Material> goblinSkins;
    public List<Material> mushroomSkins;
    public List<Material> plantSkins;
    public List<Material> wolfSkins;
}

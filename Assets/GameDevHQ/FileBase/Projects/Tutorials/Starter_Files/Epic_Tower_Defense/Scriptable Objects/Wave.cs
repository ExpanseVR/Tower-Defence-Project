using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "newWave.asset", menuName = "ScriptableObjects/new Wave")]
public class Wave : ScriptableObject
{
    public float timeBetweenSpawns;

    public List<GameObject> sequenceOfEnemies;
}

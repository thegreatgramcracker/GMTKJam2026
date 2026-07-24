using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterSet", menuName = "Scriptable Objects/EncounterSet")]
public class EncounterSet : ScriptableObject
{
    public WeightedEncounter[] encounters;

    public Encounter GetRandomEncounter()
    {
        if (encounters == null) return null;
        float sum = encounters.Sum(e => e.weight);
        float currentVal = 0f;

        float rn = Random.Range(0f, sum);

        foreach (WeightedEncounter enc in encounters)
        {
            currentVal += enc.weight;
            if (rn <= currentVal)
            {
                return enc.encounter;
            }
        }
        return null;
    }
}
[System.Serializable]
public class WeightedEncounter
{
    public Encounter encounter;
    public float weight;
}

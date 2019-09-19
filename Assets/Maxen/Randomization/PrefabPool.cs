using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabPool", menuName = "New Prefab Pool")]
public class PrefabPool : ScriptableObject
{
    [System.Serializable]
    public struct WeightedPrefab
    {
        public AnimationCurve weight;
        public GameObject prefab;
    }

    [SerializeField] protected List<WeightedPrefab> _poolItems;

    public virtual GameObject GetRandomItem(float curvePosition)
    {
        float totalWeight = 0.0f;
        foreach (WeightedPrefab item in _poolItems)
        {
            totalWeight += item.weight.Evaluate(curvePosition);
        }

        float chosenWeight = Random.Range(0.0f, totalWeight);

        int currentItemIndex = -1;
        while(chosenWeight > 0.0f)
        {
            currentItemIndex++;
            chosenWeight -= _poolItems[currentItemIndex].weight.Evaluate(curvePosition);
        }

        currentItemIndex = Mathf.Clamp(currentItemIndex, 0, _poolItems.Count - 1);
        return _poolItems[currentItemIndex].prefab;
    }
}

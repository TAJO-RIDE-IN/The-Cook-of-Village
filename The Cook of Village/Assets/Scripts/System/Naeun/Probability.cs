using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probability<T>
{
    List<ProbabilityElement> probabilityList = new List<ProbabilityElement>();

    public bool isEmpty { get { return probabilityList.Count <= 0; } }

    public class ProbabilityElement
    {
        public T target;
        public float probability;

        public ProbabilityElement(T target, float probability)
        {
            this.target = target;
            this.probability = probability;
        }
    }

    public void Add(T target, float probability)
    {
        probabilityList.Add(new ProbabilityElement(target, probability));
    }

    public T Get()
    {
        float totalProbability = 0;
        for (int i = 0; i < probabilityList.Count; i++)
            totalProbability += probabilityList[i].probability;

        float pick = Random.value * totalProbability;
        for (int i = 0; i < probabilityList.Count; i++)
        {
            if (pick < probabilityList[i].probability)
                return probabilityList[i].target;
            else
                pick -= probabilityList[i].probability;
        }
        return default(T);
    }
}

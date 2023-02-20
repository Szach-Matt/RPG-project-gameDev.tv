using System;
using GameDevTV.Saving;
using UnityEngine;


namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experencePoints = 0;

        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            experencePoints += experience;
            onExperienceGained();
        }

        public float GetPoints()
        {
            return experencePoints;
        }

        public object CaptureState()
        {
            return experencePoints;
        }

        public void RestoreState(object state)
        {
            experencePoints = (float)state;
        }
    }
}
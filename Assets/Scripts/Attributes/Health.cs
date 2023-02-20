using GameDevTV.Utils;
using RPG.Core;
using GameDevTV.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] TakeDamageEvent takeDamage;
        [SerializeField] UnityEvent onDie;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }

        BaseStats baseStats;
        LazyValue<float> health;
        bool isDead = false;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            health = new LazyValue<float>(GetInitialHealth);
        }


        private float GetInitialHealth()
        {
            return baseStats.GetStat(Stat.Health);
        }

        private void Start()
        {
            health.ForceInit();
        }


        private void OnEnable()
        {

            baseStats.onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {

            baseStats.onLevelUp -= RegenerateHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            health.value = Mathf.Max(health.value - damage, 0);

            if (health.value == 0 && !isDead)
            {
                onDie.Invoke();
                Die();
                AwardExperiance(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public void Heal(float healthToRestore)
        {
            health.value = Mathf.Min(health.value + healthToRestore, GetMaxHealthPoints());
        }

        private void AwardExperiance(GameObject instigator)
        {
            Experience XP = instigator.GetComponent<Experience>();
            if (XP == null) return;
            XP.GainExperience(baseStats.GetStat(Stat.ExperianceReward));
        }

        public float GetHealthPoints()
        {
            return health.value;
        }
        public float GetMaxHealthPoints()
        {
            return baseStats.GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return health.value / baseStats.GetStat(Stat.Health);
        }


        private void Die()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = baseStats.GetStat(Stat.Health) * (regenerationPercentage / 100);
            health.value = Mathf.Max(health.value, regenHealthPoints);
        }

        public object CaptureState()
        {
            return health.value;
        }
        public void RestoreState(object state)
        {
            health.value = (float)state;
            if (health.value == 0 && !isDead)
            {
                Die();
            }
        }
    }
}
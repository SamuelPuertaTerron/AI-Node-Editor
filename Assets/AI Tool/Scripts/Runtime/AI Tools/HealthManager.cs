using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AINodeTool {
    [AddComponentMenu("AI Node Tool/ Health Manager")]
    public class HealthManager : MonoBehaviour {
        public delegate void Damage();
        public event Damage OnTakeDamage;

        public delegate void Death();
        public event Death OnDeath;

        public float CurrentHealth { get { return m_currentHealth; } }
        public float MaxHealth { get { return maxHealth; } }

        [SerializeField] private float maxHealth = 100.0f;
        [SerializeField] private float damageDelayTime = 1.5f;
        private float m_currentHealth;

        private void Start() {
            m_currentHealth = maxHealth;
        }

        public void TakeDamage(float damage) {
            StartCoroutine(TakeDamageOverTime(damage));
        }

        private IEnumerator TakeDamageOverTime(float damage) {
            yield return new WaitForSeconds(damageDelayTime);
            TakeDamageInternal(damage);
        }

        private void TakeDamageInternal(float damage) {
            m_currentHealth -= damage;

            if (OnTakeDamage != null) OnTakeDamage();

            if (m_currentHealth <= 0) {
                if (OnDeath != null) OnDeath();
            }
        }
    }
}



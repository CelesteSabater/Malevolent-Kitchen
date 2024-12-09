using System.Collections;
using Project.RecipeTree.Runtime;
using UnityEngine;

namespace Project.RecipeTree.Runtime
{
    public class FoodSpawnManager : Singleton<FoodSpawnManager>
    {

        [Header("Summon Food Settings")]
        [SerializeField] private GameObject _foodSpawnEffect;
        [SerializeField] private GameObject _foodSpawnEffectWithoutBeam;
        [SerializeField] private GameObject _foodDestroyEffect;
        [SerializeField] private GameObject _burntFood;
        [SerializeField] private Vector3 _spawnDisplace = new Vector3(0, 0.2f, 0);
        [SerializeField] private float _summonDelay = 3;
        [SerializeField] private Vector3 _spawnNudgeValue = new Vector3(1f, 1f, 1f);
        [SerializeField] private string _soundSummonName, _soundExplosionName;

        public float GetSummonDelay() => _summonDelay;

        public void DestroyFood(GameObject go, bool instanceIngredients)
        {
            Food food = go.GetComponent<Food>();
            if (food == null)
                return;

            Vector3 pos = go.transform.position;

            if (instanceIngredients)
                CookingManager.Instance.InstanceIngredients(food);

            Instantiate(_foodDestroyEffect, pos, Quaternion.identity);
            Destroy(go);
            AudioSystem.Instance.PlaySFX(_soundExplosionName, pos);
        }

        public void DestroyGo(GameObject go)
        {
            Vector3 pos = go.transform.position;

            Instantiate(_foodDestroyEffect, pos, Quaternion.identity);
            Destroy(go);
            AudioSystem.Instance.PlaySFX(_soundExplosionName, pos);
        }

        public void DestroyAllFood(bool instanceIngredients)
        {
            GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
            foreach (GameObject f in food)
            {
                DestroyFood(f, instanceIngredients);
            }
        }

        public void InstanceFood(FoodData food, Vector3 pos, bool isBurnt)
        {
            Instantiate(_foodSpawnEffect, pos, Quaternion.identity);
            AudioSystem.Instance.PlaySFX(_soundSummonName, pos);
            StartCoroutine(Spawn(_summonDelay, food, pos, isBurnt));
        }

        public void InstanceFoodWithoutBeam(FoodData food, Vector3 pos, bool isBurnt)
        {
            Instantiate(_foodSpawnEffectWithoutBeam, pos, Quaternion.identity);
            AudioSystem.Instance.PlaySFX(_soundSummonName, pos);
            StartCoroutine(Spawn(_summonDelay, food, pos, isBurnt));
        }

        public GameObject FastInstanceFood(FoodData food, Vector3 pos, bool isBurnt)
        {
            Instantiate(_foodDestroyEffect, pos, Quaternion.identity);
            AudioSystem.Instance.PlaySFX(_soundExplosionName, pos);
            
            GameObject foodGO = null;
            AudioSystem.Instance.PlaySFX(_soundExplosionName, pos);

            if (isBurnt) foodGO = Instantiate(_burntFood, pos + _spawnDisplace, Quaternion.identity);
            else foodGO = Instantiate(food.GetPrefab(), pos + _spawnDisplace, Quaternion.identity);

            SetFoodPropierties(foodGO, food, isBurnt);
            
            return foodGO;
        }

        public GameObject FastInstanceGo(GameObject go, Vector3 pos)
        {
            Instantiate(_foodDestroyEffect, pos, Quaternion.identity);
            AudioSystem.Instance.PlaySFX(_soundExplosionName, pos);
            
            AudioSystem.Instance.PlaySFX(_soundExplosionName, pos);

            Instantiate(go, pos + _spawnDisplace, Quaternion.identity);

            SetFoodPropierties(go, null, false);
            
            return go;
        }

        IEnumerator Spawn(float delay, FoodData food, Vector3 pos, bool isBurnt)
        {
            yield return new WaitForSeconds(delay);

            GameObject foodGO = null;
            AudioSystem.Instance.PlaySFX(_soundExplosionName, pos);

            if (isBurnt) foodGO = Instantiate(_burntFood, pos + _spawnDisplace, Quaternion.identity);
            else foodGO = Instantiate(food.GetPrefab(), pos + _spawnDisplace, Quaternion.identity);

            SetFoodPropierties(foodGO, food, isBurnt);
        }

        private void SetFoodPropierties(GameObject foodGO, FoodData food, bool isBurnt)
        {
            if (food != null)
            {
                Food f = foodGO.AddComponent<Food>();
                f.SetName(food.GetFoodName());
                f.SetIsBurnt(isBurnt);
            }

            SpringToScale spring = foodGO.AddComponent<SpringToScale>();
            if (spring != null)
                spring.Nudge(_spawnNudgeValue);
        }
    }
}
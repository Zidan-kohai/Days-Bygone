using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.Base
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private List<T> prefabs = new();
        private Transform poolContainer;
        private bool autoExpand;

        private List<T> pool = new();

        public ObjectPool(T prefab, Transform container, int poolSize, bool autoExpand)
        {
            if (prefab == null)
                throw new NullReferenceException(nameof(prefab));


            if (container == null)
                throw new NullReferenceException(nameof(container));

            prefabs.Add(prefab);

            poolContainer = container;

            this.autoExpand = autoExpand;

            InitPool(prefab, poolSize);
        }

        public ObjectPool(List<T> prefabs, Transform container, int poolSize, bool autoExpand)
        {
            if (prefabs == null)
                throw new NullReferenceException(nameof(prefabs));

            if (container == null)
                throw new NullReferenceException(nameof(container));

            this.prefabs = prefabs;

            poolContainer = container;

            this.autoExpand = autoExpand;

            foreach (var prefab in prefabs)
            {
                InitPool(prefab, poolSize);
            }
        }

        private void InitPool(T prefab, int poolSize)
        {
            for (int i = 0; i < poolSize; i++)
                CreateNewObject(prefab, false);
        }

        private T CreateNewObject(T prefab, bool isActiveByDefault)
        {
            T newObject = GameObject.Instantiate(prefab, poolContainer);
            
            newObject.gameObject.SetActive(isActiveByDefault);
            
            pool.Insert((UnityEngine.Random.Range(0, pool.Count)),newObject);

            return newObject;
        }
        
        public void ReturnToPool(T target)
        {
            if (target != null)
            {
                target.gameObject.SetActive(false);
                target.transform.position = poolContainer.position;
            }
        }

        public bool TryGetFree<V>(out V element) where V : T
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].gameObject.activeInHierarchy && pool[i] is V)
                {
                    element = pool[i] as V;
                    return true;
                }
            }

            element = null;
            return false;
        }

        public void DisableAll()
        {
            foreach (var gameObject in pool)
                gameObject.gameObject.SetActive(false);
        }

        public T GetFree()
        {
            if (TryGetFree(out T element))
            {
                element.gameObject.SetActive(true);
                return element;
            }

            if (autoExpand)
                return CreateNewObject(prefabs[UnityEngine.Random.Range(0, prefabs.Count)],true);

            throw new Exception("There is no free element in pool");
        }

        public T GetFree<V>() where V : T
        {
            if (TryGetFree(out V element))
            {
                element.gameObject.SetActive(true);
                return element;
            }

            if (autoExpand)
            {
                T prefab = prefabs.Find(prefab => prefab.GetType() == typeof(V));

                if (prefab != null) return CreateNewObject(prefab, true);

                return CreateNewObject(prefab, true);
            }

            throw new Exception("There is no free element in pool");
        }

        public T GetFreeWithout<V>() where V : T
        {
            if (TryGetFree(out T element) && element.GetType() != typeof(V))
            {
                element.gameObject.SetActive(true);
                return element;
            }

            if (autoExpand)
            {
                T prefab = prefabs.Find(prefab => prefab.GetType() != typeof(V));

                if (prefab != null) return CreateNewObject(prefab, true);

                return CreateNewObject(prefab, true);
            }

            throw new Exception($"There is no free {typeof(V)} element in pool");
        }
    }
}

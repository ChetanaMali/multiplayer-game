using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalServices
{
    public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
    {
        private static T instance;

        public static T Instance => instance;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = (T)this;
            }
        }
    }
}
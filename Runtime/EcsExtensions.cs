﻿using Unity.Entities;
using UnityEngine;

namespace EcsExtensions.Runtime
{
    public static class EcsExtensions
    {
        public static T Inject<T>(this SystemBase system) where T : ComponentSystemBase
        {
            return system.World.GetOrCreateSystem<T>();
        }


        public static void Set<T>(this EntityManager entityManager, Entity entity, T component)
            where T : struct, IComponentData
        {
            if (entityManager.HasComponent<T>(entity))
            {
                entityManager.SetComponentData<T>(entity, component);
            }
            else
            {
                entityManager.AddComponentData<T>(entity, component);
            }
        }

        public static bool TryGet<T>(this EntityManager entityManager, Entity entity, out T component)
            where T : struct, IComponentData
        {
            component = default;
            if (!entityManager.HasComponent<T>(entity))
            {
                return false;
            }

            component = entityManager.GetComponentData<T>(entity);
            return true;
        }

        public static bool TryGetObject<T>(this EntityManager entityManager, Entity entity, out T component)
            where T : Component
        {
            component = default;
            if (!entityManager.HasComponent<T>(entity))
            {
                return false;
            }

            component = entityManager.GetComponentObject<T>(entity);
            return true;
        }

        public static bool Remove<T>(this EntityManager entityManager, Entity entity)
            where T : struct, IComponentData
        {
            if (entityManager.HasComponent<T>(entity))
            {
                entityManager.RemoveComponent<T>(entity);
                return true;
            }

            return false;
        }
    }
}
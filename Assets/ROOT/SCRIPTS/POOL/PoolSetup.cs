using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Pool/PoolSetup")]
public class PoolSetup : MonoBehaviour
{

	#region Unity scene settings
	[SerializeField] private PoolManager.PoolPart[] pools;
	#endregion

	#region Methods
	void OnValidate()
	{
		for (int i = 0; i < pools.Length; i++)
		{
			pools[i].name = pools[i].prefab.name;
		}
	}

	void Awake()
	{
		Initialize();
	}

	void Initialize()
	{
		PoolManager.Initialize(pools);
	}
	#endregion
}

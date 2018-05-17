﻿using UnityEngine;

namespace TankProto
{
	/// <summary>
	/// Class for handling player/enemy health.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public class HealthHandler : MonoBehaviour
	{
		[Header("Health Variables")]
		[SerializeField] private MasterEntity _instance = MasterEntity.None;
		[SerializeField] [Range(0, 1000)] private float _healthPoints = 150;
		[SerializeField] [Range(0, 1)] private float _armor = 0.5f;

		void OnTriggerEnter(Collider bumpCollider)
		{
			Projectile projectile = bumpCollider.GetComponent<Projectile>();
			if (projectile == null) return;

			_healthPoints -= (1 - _armor) * projectile.Damage;
			projectile.BlowUp();

			if (_healthPoints <= 0) HandleDefeat();
		}

		private void HandleDefeat()
		{
			switch (_instance)
			{
				case MasterEntity.Enemy:
					GetComponent<Projectile>().BlowUp();
					break;
				case MasterEntity.Player:
					FindObjectOfType<SceneLoadingHandler>().LoadNextScene();
					break;
			}
		}
	}
}

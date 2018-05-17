﻿using UnityEngine;

namespace TankProto
{
	[RequireComponent(typeof(MovementHandler))]
	[RequireComponent(typeof(WeaponHandler))]
	public class InputHandler : MonoBehaviour
	{
		private MovementHandler _movementHandler = null;
		private WeaponHandler _weaponHandler = null;

		private bool _isInputEnabled = true;

		void Start()
		{
			_movementHandler = GetComponent<MovementHandler>();

			_weaponHandler = GetComponent<WeaponHandler>();
			_weaponHandler.DisableInputEvent += DisableInput;
			_weaponHandler.EnableInputEvent += EnableInput;
		}

		void FixedUpdate()
		{
			HandleMovement();
			HandleRotation();
			ChangeWeapon();
			HandleWeapons();
		}

		private void HandleMovement()
		{
			float deltaZ = Input.GetAxis(GlobalVariables.VerticalAxis);
			_movementHandler.Move(deltaZ);
		}

		private void HandleRotation()
		{
			float deltaY = Input.GetAxis(GlobalVariables.HorizontalAxis);
			_movementHandler.Rotate(deltaY);
		}

		private void ChangeWeapon()
		{
			if (!_isInputEnabled) return;

			if (Input.GetKeyDown(KeyCode.E))
			{
				_weaponHandler.RotateCylinder(180);
				_weaponHandler.UpdateCurrentWeapon();
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				_weaponHandler.RotateCylinder(-180);
				_weaponHandler.UpdateCurrentWeapon();
			}
		}

		private void HandleWeapons()
		{
			if (!_isInputEnabled) return;

			if (Input.GetKey(KeyCode.X))
			{
				_weaponHandler.HandleProjectileLaunching();
			}
		}

		private void EnableInput()
		{
			_isInputEnabled = true;
		}

		private void DisableInput()
		{
			_isInputEnabled = false;
		}
	}
}

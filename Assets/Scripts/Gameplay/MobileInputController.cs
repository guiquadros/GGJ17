﻿using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class MobileInputController : MonoBehaviour
    {
        private bool holdingDownLeft;
        private bool holdingDownRight;

        public void OnRightScreenUp()
        {
            this.holdingDownRight = false;

            GameManager.Instance.entities.spaceshipRight.spaceshipMove.ResetDirection();
        }

        public void OnLeftScreenUp()
        {
            this.holdingDownLeft = false;

            GameManager.Instance.entities.spaceshipLeft.spaceshipMove.ResetDirection();
        }

        public void OnRightScreenDown()
        {
            if (holdingDownRight) return;

            this.holdingDownRight = true;

            GameManager.Instance.entities.spaceshipRight.spaceshipMove.ReverseInitialDirection();
        }

        public void OnLeftScreenDown()
        {
            if (holdingDownLeft) return;

            this.holdingDownLeft = true;

            GameManager.Instance.entities.spaceshipLeft.spaceshipMove.ReverseInitialDirection();
        }
    }
}
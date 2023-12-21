﻿using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Button = "Fire1";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp()
        {
            return SimpleInput.GetButtonUp(Button);
        }

        protected static Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }
    }
}
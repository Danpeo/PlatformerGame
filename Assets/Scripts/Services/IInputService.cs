using Infrastructure.Services;
using UnityEngine;

namespace Services
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}
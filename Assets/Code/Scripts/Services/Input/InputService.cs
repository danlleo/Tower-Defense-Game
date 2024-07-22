namespace Services.Input
{
    public abstract class InputService : IInputService
    {
        public bool IsInteractButtonUp() => UnityEngine.Input.GetMouseButtonUp(0);
    }
}
public interface IInteractable
{
    string InteractionPrompt { get;} // All objects using the interaction interface must use 
    void Interact(); // Defines a method that all interactable objects must have
}

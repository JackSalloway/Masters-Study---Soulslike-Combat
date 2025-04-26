public interface IInteractable
{
    string InteractionPrompt { get;} // Returns the verb related to the action (example: open, pick up, pull)
    void Interact(); // Method to interact with the object (example: open door, pick up item)
}

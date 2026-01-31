using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private PossessionSystem possessionSystem;
    [SerializeField] private ControllableCharacter startCharacter;

    private void Start()
    {
        possessionSystem.SetInitialCharacter(startCharacter);
    }
}

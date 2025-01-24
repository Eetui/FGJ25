using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bubblePrefab;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private KeyCode _spawnKey = KeyCode.Mouse1;

    [SerializeField] private ExampleCharacterController _characterController;

    private float _spawnDistance = 2.0f;
    private GameObject _currentBubble;
    private float _bubbleScale = 0.1f;
    private float _scaleIncreaseRate = 1.0f;

    private void Update()
    {
        if (Input.GetKey(_spawnKey))
        {
            if (_currentBubble == null)
            {
                SpawnSphere();
                _characterController.TransitionToState(CharacterState.Disabled);
            }
            else
            {
                GrowSphere();
            }
        }

        if (Input.GetKeyUp(_spawnKey))
        {
            _characterController.TransitionToState(CharacterState.Default);
            ReleaseSphere();
        }
    }

    private void SpawnSphere()
    {
        if (_bubblePrefab)
        {
            Vector3 spawnPosition = (transform.position + _offset) + transform.forward * _spawnDistance;
            _currentBubble = Instantiate(_bubblePrefab, spawnPosition, Quaternion.identity);
            _currentBubble.transform.localScale = Vector3.one * _bubbleScale;
        }
        else
        {
            Debug.LogError("Sphere prefab is not assigned!");
        }
    }

    private void GrowSphere()
    {
        if (_currentBubble)
        {
            _bubbleScale += _scaleIncreaseRate * Time.deltaTime;
            _currentBubble.transform.localScale = Vector3.one * _bubbleScale;
        }
    }

    private void ReleaseSphere()
    {
        if (_currentBubble)
        {
            Bubble bubble = _currentBubble.GetComponent<Bubble>();
            if (bubble)
            {
                bubble.EnablePhysics();
            }

            _currentBubble = null;
            _bubbleScale = 0.1f;
        }
    }
}

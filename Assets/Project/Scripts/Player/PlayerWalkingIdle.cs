using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Project.Scripts.Player
{
    public class PlayerWalkingIdle : MonoBehaviour
    {
        [SerializeField] private Transform[] idlingPoints;
        [SerializeField] private Transform startPoint;
        [SerializeField] private CinemachineVirtualCamera _camera;
        private int _currentTarget;
        private PlayerAnimations _animations;
        private NavMeshAgent _agent;
        private PlayerActions _actions;
        private PlayerHealth _health;
        private PlayerDistanceCounter _counter;

        private void Start()
        {
            _currentTarget = 0;
            _animations = GetComponent<PlayerAnimations>();
            _agent = GetComponent<NavMeshAgent>();
            _actions = GetComponent<PlayerActions>();
            _counter = GetComponent<PlayerDistanceCounter>();
            _health = GetComponent<PlayerHealth>();
            _actions.OnStartGame += StopIdling;
            _actions.dust.Stop();
            StartCoroutine(GotoCoroutine(idlingPoints[_currentTarget]));
        }

        public void StopIdling()
        {
            _actions.OnStartGame -= StopIdling;
            StopAllCoroutines();
            StartCoroutine(GotoStartCoroutine());
        }

        private IEnumerator GotoStartCoroutine()
        {
            _agent.SetDestination(startPoint.position);
            _agent.speed = 3;
            _agent.acceleration = 8;
            _animations.SetState(2);
            while (Vector3.Distance(transform.position, startPoint.position) > 0.1f)
            {
                yield return null;
            }

            _agent.enabled = false;
            transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);
            _actions.StartPlayer();
            _camera.Priority -= 3;
            _counter.enabled = true;
            _health.StartHealth();
        }

        private IEnumerator GotoCoroutine(Transform target)
        {
            yield return new WaitForSeconds(Random.Range(0,5));
            _actions.dust.Play();
            _animations.SetState(1);
            _agent.SetDestination(target.position);
            while (Vector3.Distance(transform.position, target.position) > 0.2f)
            {
                yield return null;
            }
            _animations.SetState(0);
            _actions.dust.Stop();
            yield return new WaitForSeconds(Random.Range(0, 4));
            _currentTarget = (_currentTarget + 1) % idlingPoints.Length;
            StartCoroutine(GotoCoroutine(idlingPoints[_currentTarget]));
        }
        
    }
}

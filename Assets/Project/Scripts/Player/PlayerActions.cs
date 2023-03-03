using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.VoiceRecognition;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerActions : MonoBehaviour
    {
        [Header("Lanes Settings")] [SerializeField]
        private int lanesNumber;

        [SerializeField] private float laneWidth;

        [Header("Player Movement Settings")] public float speed;
        [SerializeField] private float moveTime = 1f;
        [SerializeField] private AnimationCurve curve;

        [Header("Jump Settings")] [SerializeField]
        private float jumpTime = 1f;

        [SerializeField] private float jumpHeight = 10f;

        public Action OnBark;
        public Action OnHurt;
        public Action OnDead;

        [Header("OtherSettings")] [SerializeField]
        private Transform model;

        [SerializeField] private ParticleSystem dust;

        private int _lane;
        private bool moveLock;
        private VoiceRecognitionAPISelector apiSelector;


        private void Start()
        {
            _lane = lanesNumber / 2;
            OnDead += Dead;
            SetActions();
        }

        private void Dead()
        {
            apiSelector.TurnOff();
            speed = 0;
        }

        private void Update()
        {
            transform.position += Time.deltaTime * speed * Vector3.forward;
        }

        private void SetActions()
        {
            Dictionary<string, Action> wordsToActions = new();
            wordsToActions.Add("derecha", MoveRight);
            wordsToActions.Add("izquierda", MoveLeft);
            wordsToActions.Add("salta", Jump);
            wordsToActions.Add("right", MoveRight);
            wordsToActions.Add("left", MoveLeft);
            wordsToActions.Add("jump", Jump);
            
            apiSelector = new VoiceRecognitionAPISelector();
            apiSelector.MapActions(wordsToActions);
        }

        private void CommandRecognized()
        {
            OnBark?.Invoke();
        }

        private void MoveRight()
        {
            CommandRecognized();
            if (_lane < lanesNumber - 1)
            {
                StartCoroutine(MoveToLaneCoroutine(true));
                _lane++;
            }
        }

        private void MoveLeft()
        {
            CommandRecognized();
            if (_lane > 0)
            {
                StartCoroutine(MoveToLaneCoroutine(false));
                _lane--;
            }
        }

        private void Jump()
        {
            CommandRecognized();
            StartCoroutine(JumpCoroutine());
        }

        private IEnumerator MoveToLaneCoroutine(bool right)
        {
            if (moveLock) yield break;
            moveLock = true;

            float direction = right ? 1 : -1;
            Vector3 startRotation = model.eulerAngles;

            float xMove = laneWidth * direction;
            float xStart = transform.position.x;
            float xTarget = transform.position.x + xMove;
            float finalRotation = Mathf.Rad2Deg * Mathf.Atan2(laneWidth, speed) * direction;

            float timePassed = 0;
            while (timePassed < moveTime)
            {
                float delta = timePassed / moveTime;
                transform.position = new Vector3(xStart + xMove * delta, transform.position.y, transform.position.z);
                model.eulerAngles = new Vector3(transform.eulerAngles.x, curve.Evaluate(delta) * finalRotation,
                    transform.eulerAngles.z);
                timePassed += Time.deltaTime;
                yield return null;
            }

            transform.position = new Vector3(xTarget, transform.position.y, transform.position.z);
            model.eulerAngles = startRotation;
            moveLock = false;
        }


        IEnumerator JumpCoroutine()
        {
            moveLock = true;
            float gravity = 9.81f;
            float timePassed = 0.0f;
            float yStart = transform.position.y;
            float yJump = jumpHeight;
            dust.Stop();
            while (timePassed < jumpTime)
            {
                yJump += -(gravity * Time.deltaTime);
                Vector3 position = transform.position;
                position.y = yStart + yJump * (timePassed / jumpTime);
                transform.position = position;
                timePassed += Time.deltaTime;
                yield return null;
            }

            transform.position = new Vector3(transform.position.x, yStart, transform.position.z);
            dust.Play();
            moveLock = false;
        }

        private void MoveToNextLane(bool right)
        {
            transform.position += new Vector3(laneWidth * (right ? 1 : -1), 0, 0);
        }
    }
}
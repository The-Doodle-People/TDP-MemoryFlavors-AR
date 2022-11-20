using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/*
 * 'LogicalStateMachineBehaviour' extends states of the animator (and therefore is only usable for this states) by
 * sending global messages over the 'GlobalMessageEventManager' on enter or exit of a state.
 * This allows building simple statemachines with an animator and altering the game with unity events at 
 * an 'GlobalMessagEventReceiver'.
 */


namespace DT {
	public class LogicalStateMachineBehaviour : StateMachineBehaviour {


		public string OnEnterMessage = "Enter";
		public string OnExitMessage = "Exit";


		// PRAGMA MARK - StateMachineBehaviour Lifecycle
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			this.Animator = animator;

			this.OnStateEntered();
			this._active = true;
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			this.Animator = animator;

			this._active = false;
			this.OnStateExited();
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			this.OnStateUpdated();
		}


		// PRAGMA MARK - Internal
		private bool _active = false;

		protected Animator Animator { get; private set; }

		void OnDisable() {
			if (this._active) {
				this.OnStateExited();
			}
		}

		protected virtual void OnStateEntered() {
			GlobalMessageEventManager.sendToReceivers (OnEnterMessage);
		}
		protected virtual void OnStateExited() {
			GlobalMessageEventManager.sendToReceivers (OnExitMessage);
		}
		protected virtual void OnStateUpdated() {}
	}
}
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class InteractableScreenEvent : UnityEvent<InteractableScreen> { }

	[CreateAssetMenu(
	    fileName = "InteractableScreenVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "InteractableScreen",
	    order = 120)]
	public class InteractableScreenVariable : BaseVariable<InteractableScreen, InteractableScreenEvent>
	{
	}
}
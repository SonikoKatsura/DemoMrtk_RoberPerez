using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionHandlerScript : MonoBehaviour,
    IMixedRealityInputActionHandler,
    IMixedRealityFocusHandler
{

    //EVENTO (DELEGADO)   --> Aumenta la cantidad de Gemas recogidas
    public delegate void GemCollected();
    public static event GemCollected onGemCollected;  //(EVENTO)
    //EVENTO (DELEGADO)   --> Muestra el texto al tocar la puerta rota
    public delegate void BrokenDoor();
    public static event BrokenDoor onBrokenDoor;  //(EVENTO)

    private bool isFocused = false;

    private void OnEnable()
    {
        CoreServices.InputSystem.RegisterHandler<IMixedRealityInputActionHandler>(this);
        CoreServices.InputSystem.RegisterHandler<IMixedRealityFocusHandler>(this);
    }

    private void OnDisable()
    {
        CoreServices.InputSystem.UnregisterHandler<IMixedRealityInputActionHandler>(this);
        CoreServices.InputSystem.UnregisterHandler<IMixedRealityFocusHandler>(this);
    }

    void IMixedRealityInputActionHandler.OnActionStarted(BaseInputEventData eventData)
    {
        if (!isFocused)
        {
            return;
        }

        if (eventData.MixedRealityInputAction.Description == "Select") {
            //Debug.Log("Funciona");            

            switch (gameObject.tag) {
                case "Menu": SceneManager.LoadScene("MainMenu"); break;
                case "Intro": SceneManager.LoadScene("Intro"); break;
                case "Game": SceneManager.LoadScene("Game"); break;
                case "Exit": Application.Quit(); break;
                case "Credits": SceneManager.LoadScene("Credits"); break;
                case "Door": SceneManager.LoadScene("WinScene"); break;
                case "BrokenDoor":
                    //Evento Aumenta la cantidad de Gemas recogidas
                    if (onBrokenDoor != null)
                        onBrokenDoor();
                    break;
                case "Item":
                    gameObject.GetComponent<SimpleCollectibleScript>().Collect();
                    //Evento Aumenta la cantidad de Gemas recogidas
                    if (onGemCollected != null)
                        onGemCollected();
                    break;
                //default:  break;
            }
        }
    }

    void IMixedRealityInputActionHandler.OnActionEnded(BaseInputEventData eventData) { }

    void IMixedRealityFocusHandler.OnFocusEnter(FocusEventData eventData)
    {
        isFocused = eventData.NewFocusedObject.GetHashCode() == gameObject.GetHashCode();
    }

    void IMixedRealityFocusHandler.OnFocusExit(FocusEventData eventData)
    {
        isFocused = false;
    }
}

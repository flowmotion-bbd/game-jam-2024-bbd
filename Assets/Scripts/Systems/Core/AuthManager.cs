using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance { get; private set; }
    public string PlayerUsername { get; set; }

    private async void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            await InitializeAuth();
            PlayerUsername = AuthenticationService.Instance.PlayerName;
        }
    }

    public async Task InitializeAuth()
    {
        await UnityServices.InitializeAsync();
        await SignInAnonymouslyAsync();
    }

    public async Task SignInAnonymouslyAsync()
    {
        if (!IsSignedIn())
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    public async Task UpdatePlayerNameAsync(string playerName)
    {
        await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
    }

    public bool IsSignedIn()
    {
        return AuthenticationService.Instance.IsSignedIn;
    }
}

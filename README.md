# Firebase.Authentication

A [Firebase](https://firebase.google.com/) Authentication package for the Unity Engine.

## Installing

### Via Unity Package Manager and OpenUPM

- Open your Unity project settings
- Add the OpenUPM package registry
- Add the OpenUPM scope: `com.openupm`
- Add the Firebase scope: `com.firebase`
![scoped-registries](Firebase.Authentication/Packages/com.firebase.authentication/~Documentation/images/package-manager-scopes.png)
- Open the Unity Package Manager window
- Change the Registry from Unity to `My Registries`
- Add the `Firebase.Authentication` package

### Via Unity Package Manager and Git url

- Open your Unity Package Manager
- Add package from git url: `https://github.com/StephenHodgson/com.firebase.authentication.git#upm`

## Getting Started

```csharp
var firebaseClient = new FirebaseAuthenticationClient("apiKey", "hello.firebase.com");

FirebaseUser firebaseUser;

try
{
    firebaseUser = await firebaseClient.SignInWithEmailAndPasswordAsync("username", "password");
}
catch (Exception e)
{
    Debug.LogError(e);
    return;
}

Debug.Log($"Signed in as {firebaseUser.Info.DisplayName}");

await firebaseClient.SignOutAsync();
```

## Additional Packages

- Firebase.Database (Coming soon!)
- Firebase.Storage (Coming soon!)

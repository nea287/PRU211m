using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using System;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Firebase.Database;

public class AuthManager : MonoBehaviour
{

    //public TMP_InputField loginEmail, loginPassword, 
    //                        registerName, registerEmail, registerPassword, registerConfirmPassword,
    //                        emailToRecoverPassword;
    //public TMP_Text warningText, warningTitleText;
    //public GameObject warningPanel, loginPanel, registerPanel, forgetPasswordPanel;
    //public Toggle rememberMe;

    //public void Login()
    //{
    //    OpenLoginPanel();
    //    // input field not null
    //    if (string.IsNullOrEmpty(loginEmail.text)
    //        && string.IsNullOrEmpty(loginPassword.text))
    //    {
    //        ShowWarningMessage("Empty field", "Plz enter all field");
    //        return;
    //    }

    //    //Do login

    //}

    //public void Register()
    //{
    //    OpenRegisterPanel();
    //    // check input field not null
    //    if (string.IsNullOrEmpty(registerEmail.text)
    //        && string.IsNullOrEmpty(registerPassword.text)
    //        && string.IsNullOrEmpty(registerConfirmPassword.text)
    //        && string.IsNullOrEmpty(registerName.text))
    //    {
    //        ShowWarningMessage("Empty field", "Plz enter all field");
    //        return;
    //    }

    //    // Do register 
    //}

    //public void ForgetPassword()
    //{
    //    OpenForgetPasswordPanel();
    //    // Check all field
    //    if (string.IsNullOrEmpty(emailToRecoverPassword.text))
    //    {
    //        ShowWarningMessage("Empty field", "Plz enter email");
    //        return;
    //    }

    //    // Do forget Password
    //}

    //private void ShowWarningMessage(string title, string msg)
    //{
    //    warningText.text = msg;
    //    warningTitleText.text = title;
    //    warningPanel.SetActive(true);
    //}

    //public void Logout()
    //{

    //}

    //public void OpenLoginPanel()
    //{
    //    loginPanel.SetActive(true);
    //    registerPanel.SetActive(false);
    //    forgetPasswordPanel.SetActive(false);
    //}

    //public void OpenRegisterPanel()
    //{
    //    loginPanel.SetActive(false);
    //    registerPanel.SetActive(true);
    //    forgetPasswordPanel.SetActive(false);
    //}

    //public void OpenForgetPasswordPanel()
    //{
    //    loginPanel.SetActive(false);
    //    registerPanel.SetActive(false);
    //    forgetPasswordPanel.SetActive(true);
    //}

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public TMP_Text warningText;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    

    //Register variables
    [Header("Register")]
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;

    private DatabaseReference databaseReference;
    private string userID;

    void Awake()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningText.text = message;
            UIManager.instance.WarningScreen();
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = new FirebaseUser(LoginTask.Result.User);
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);

            SceneManager.LoadScene("Level1");
        }
    }

    private IEnumerator Register(string _email, string _password)
    {
        if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningText.text = "Password Does Not Match!";
            UIManager.instance.WarningScreen();
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningText.text = message;
                UIManager.instance.WarningScreen();
            }
            else
            {
                //User has now been created
                //Now get the result
                User = new FirebaseUser(RegisterTask.Result.User);
                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _email };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningText.text = "Username Set Failed!";
                        UIManager.instance.WarningScreen();
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        Player player = new Player(User.Email, 0, 0, 0);
                        string json = JsonUtility.ToJson(player);
                        databaseReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
                        UIManager.instance.LoginScreen();
                    }
                }
            }
        }
    }
}
using Foundation;

namespace EntitlementsTest;

public partial class MainPage
{
    private bool _isSignedIn;
    public bool IsSignedIn
    {
        get => _isSignedIn;
        set
        {
            if (_isSignedIn == value)
                return;

            _isSignedIn = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSignedOut));
        }
    }

    public bool IsSignedOut => !IsSignedIn;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnSignInClicked(object? sender, EventArgs e)
    {
        Firebase.Auth.Auth.DefaultInstance!.SignInAnonymously((result, error) =>
        {
            if (result?.User is not null)
            {
                DisplayAlert("Result", "Success!", "OK");
                IsSignedIn = true;
            }
            else if (error is not null)
            {
                DisplayAlert("Result", error.LocalizedFailureReason + " " + error.LocalizedDescription, "OK");
                IsSignedIn = false;
            }
            else
            {
                DisplayAlert("Result", "Failed for some reason", "OK");
            }
        });
    }

    private void OnSignOutClicked(object? sender, EventArgs e)
    {
        Firebase.Auth.Auth.DefaultInstance!.SignOut(out NSError? error);
        if (error is null)
            IsSignedIn = false;
    }
}
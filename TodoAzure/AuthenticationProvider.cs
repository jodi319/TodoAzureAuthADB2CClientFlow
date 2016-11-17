using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace TodoAzure
{
	public class AuthenticationProvider : IAuthenticate
	{
		public static PublicClientApplication ActiveDirectoryB2CAuthenticationClient { get; private set; }

		public static MobileServiceUser User { get; private set; }

		public AuthenticationProvider()
		{
			ActiveDirectoryB2CAuthenticationClient = new PublicClientApplication(Constants.ApplicationID);
		}

		public void Initialize(IPlatformParameters parameters)
		{
			ActiveDirectoryB2CAuthenticationClient.PlatformParameters = parameters;
		}

        private static Page page;

        public async Task<bool> LoginAsync(bool useSilent = false)
		{
			bool success = false;
			try
			{
				AuthenticationResult authenticationResult;

				if (useSilent)
				{
					authenticationResult = await ActiveDirectoryB2CAuthenticationClient.AcquireTokenSilentAsync(Constants.Scopes);
				}
				else
				{
					authenticationResult = await ActiveDirectoryB2CAuthenticationClient.AcquireTokenAsync(
						Constants.Scopes,
						string.Empty,
						UiOptions.SelectAccount,
						string.Empty,
						null,
						Constants.Authority,
						Constants.SignUpSignInPolicy);
				}

				if (User == null)
				{
					var payload = new JObject();
					if (authenticationResult != null && !string.IsNullOrWhiteSpace(authenticationResult.Token))
					{
						payload["access_token"] = authenticationResult.Token;
					}

					User = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory, payload);
					success = true;
				}
			}
			catch (MsalException ex)
			{
                //throw ex;
                if (ex.Message != null && ex.Message.Contains("AADB2C90118"))
                {
                    await OnForgotPassword();
                }
                if (ex.ErrorCode != "authentication_canceled")
                {
                    await page.DisplayAlert("An error has occurred", "Exception message: " + ex.Message, "Dismiss");
                }
            }
			return success;
		}

		public async Task<bool> LogoutAsync()
		{
			bool success = false;
			try
			{
				if (User != null)
				{
					await TodoItemManager.DefaultManager.CurrentClient.LogoutAsync();
					ActiveDirectoryB2CAuthenticationClient.UserTokenCache.Clear(Constants.ApplicationID);
					User = null;
					success = true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return success;
		}

        public async Task OnForgotPassword()
        {
            try
            {
                await ActiveDirectoryB2CAuthenticationClient.AcquireTokenAsync(
                  Constants.Scopes,
                  string.Empty,
                  UiOptions.SelectAccount,
                  string.Empty,
                  null,
                  Constants.Authority,
                  Constants.ResetPasswordPolicy);
            }
            catch (MsalException)
            {
                // Do nothing - ErrorCode will be displayed in OnLoginButtonClicked
                await page.DisplayAlert("OnForgotPassword", "Exception" , "Dismiss");
            }
        }
    }
}


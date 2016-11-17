namespace TodoAzure
{
	public static class Constants
	{
		// Replace strings with your mobile services and gateway URLs.
		public static readonly string ApplicationURL = @"https://coastwatch.azurewebsites.net";

		public static readonly string ApplicationID = "6de1a882-47fc-4d27-bb83-a93e2f403727";
		public static readonly string[] Scopes = { ApplicationID };
		public static readonly string SignUpSignInPolicy = "B2C_1_CWSignInSignUpPolicies";
		public static readonly string Authority = "https://login.microsoftonline.com/CoastWatchB2C.onmicrosoft.com";
        public static readonly string ResetPasswordPolicy = "B2C_1_CWPasswordResetPolicy";
    }
}

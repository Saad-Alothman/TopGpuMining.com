using System;
using Google.Authenticator;

namespace CreaDev.Framework.Core.Security.Services
{
    public class TwoFactorAuthenticationService
    {
        public static SetupCode GenerateCode(string issureId, string userAccountTitle, string userAccountSecreteKey,
            int qrCodeWidth, int qrCodeHeight)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            SetupCode setupInfo = tfa.GenerateSetupCode(issureId, userAccountTitle, userAccountSecreteKey, qrCodeWidth, qrCodeHeight);
            return setupInfo;

        }

        public static bool ValidateCode(string userAccountSecreteKey, string code)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            bool isCorrectPin = tfa.ValidateTwoFactorPIN(userAccountSecreteKey, code, new TimeSpan(0, 15, 0));
            return isCorrectPin;
        }
    }
}

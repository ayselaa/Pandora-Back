namespace PandoraWebMvc.ViewModels.Account
{
    public class AccountAllVM
    {
        public ForgotPasswordVM Password { get; set; }
        public LoginVM Login { get; set; }
        public RegisterVM Register { get; set; }
        public ResetPasswordVM ResetPassword { get; set; }
    }
}

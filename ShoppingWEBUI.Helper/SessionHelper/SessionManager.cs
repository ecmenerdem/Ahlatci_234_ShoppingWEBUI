using Microsoft.AspNetCore.Http;
using ShoppingWEBUI.Core.DTO;
using ShoppingWEBUI.Core.ViewModel;


namespace ShoppingWEBUI.Helper.SessionHelper
{
    public class SessionManager
    {
      
        public static LoginDTO? LoggedUser 
        {
            get => AppHttpContext.Current.Session.GetObject<LoginDTO>("LoginDTO");
            set => AppHttpContext.Current.Session.SetObject("LoginDTO", value);
        }

       
    }
}

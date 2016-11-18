//using Microsoft.WindowsAzure.MobileServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xamarin.Forms;
//using TodoAzure;
//using TodoAzure.UWP;

//[assembly: Dependency(typeof(Authentication))]
//namespace TodoAzure.UWP
//{
//    public class Authentication : IAuthenticate
//    {
//        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
//        {
//            try
//            {
//                var user = await client.LoginAsync(provider);

//                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
//                Settings.UserId = user?.UserId ?? string.Empty;

//                return user;
//            }
//            catch (Exception e)
//            {
//                e.Data["method"] = "LoginAsync";
//                //Xamarin.Insights.Report(e);
//            }

//            return null;
//        }

//    }
//}

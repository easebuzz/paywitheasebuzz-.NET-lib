using System;
using System.Web;
using System.Web.UI;

namespace easebuzz_.net
{

    public partial class payout : System.Web.UI.Page
	{   
		public string salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
        public string Key = System.Configuration.ConfigurationSettings.AppSettings["key"];
      
        public string env = "prod";
		//payoutAPI
        public void payoutAPICall(object sender, EventArgs args)
        {
            
			string merchant_email = Request.Form["merchant_email"].Trim();
            
			string payout_date = Request.Form["payout_date"].Trim();
            Easebuzz t = new Easebuzz(salt, Key, env);
            string strForm = t.payoutAPI(merchant_email, payout_date);
           
 
            Response.Write(strForm);
        }
    }
}

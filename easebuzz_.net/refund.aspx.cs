using System;
using System.Web;
using System.Web.UI;

namespace easebuzz_.net
{

    public partial class refund : System.Web.UI.Page
	{   
		public string salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
        public string Key = System.Configuration.ConfigurationSettings.AppSettings["key"];

        public string env = "prod";

		public void RefundApiCall(object sender, EventArgs args)
        {
			string txnid= Request.Form["txnid"].Trim();
			string refund_amount=Request.Form["refund_amount"].Trim();
			string phone=Request.Form["phone"].Trim();
			string amount=Request.Form["amount"].Trim();
			string email=Request.Form["email"].Trim();

			Easebuzz t = new Easebuzz(salt, Key, env, "false");
            string strForm = t.RefundAPI(txnid, refund_amount, phone, amount, email);
			Response.Write(strForm);


        }
    }
}

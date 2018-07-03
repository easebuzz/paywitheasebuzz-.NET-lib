using System;
using System.Web;
using System.Web.UI;

namespace easebuzz_.net
{

    public partial class transaction_byDate : System.Web.UI.Page
    {
		public string salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
        public string Key = System.Configuration.ConfigurationSettings.AppSettings["key"];
      

        public string env = "prod";

		public void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Request.QueryString["params"]);
        }

		//transactionDateAPI
        public void transactionDateAPICall(object sender, EventArgs args)
        {
			string merchant_email = Request.Form["email"].Trim();
            
			string transaction_date = Request.Form["transaction_date"].Trim();
            Easebuzz t = new Easebuzz(salt, Key, env);
            string strForm = t.transactionDateAPI(merchant_email, transaction_date);
            
            Response.Write(strForm);

        }
    }
}

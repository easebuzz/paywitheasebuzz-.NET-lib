using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
namespace easebuzz_.net
{

	public partial class transactional_data : System.Web.UI.Page
	{
		public string salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
        public string Key = System.Configuration.ConfigurationSettings.AppSettings["key"];
      
		public string env = "prod";



        

		//transactional api
		public void transactionAPICall(object sender, EventArgs args)
		{
			
			string txnid = Request.Form["txnid"].Trim();
			string amount = Request.Form["amount"].Trim();
			string email = Request.Form["email"].Trim();
			string phone = Request.Form["phone"].Trim();
            
			Easebuzz t = new Easebuzz(salt, Key, env);
			string strForm = t.transactionAPI(txnid, amount, email, phone);

			Response.Write(strForm);


		}




	}
}

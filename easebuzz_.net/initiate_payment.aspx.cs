using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace easebuzz_.net
{
	public partial class initiate_payment : System.Web.UI.Page
    {
		public string salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
        public string Key = System.Configuration.ConfigurationSettings.AppSettings["key"];

		public string env = System.Configuration.ConfigurationSettings.AppSettings["env"];
		public string is_enable_iframe = System.Configuration.ConfigurationSettings.AppSettings["enable_iframe"];

		public string accessKey;

		//initiate payment called
		public void button1Clicked(object sender, EventArgs args)
        {   //form the mandatory fields 
			string amount = Request.Form["amount"].Trim();
			string firstname = Request.Form["firstname"].Trim();
			string email = Request.Form["email"].Trim();
			string phone = Request.Form["phone"].Trim();
			string productinfo = Request.Form["productinfo"].Trim();
			string surl = Request.Form["surl"].Trim();
			string furl = Request.Form["furl"].Trim();
			string Txnid = Request.Form["Txnid"].Trim();
			string UDF1 = Request.Form["udf1"].Trim();
			string UDF2 = Request.Form["udf2"].Trim();
			string UDF3 = Request.Form["udf3"].Trim();
			string UDF4 = Request.Form["udf4"].Trim();
			string UDF5 = Request.Form["udf5"].Trim();

			string UDF6 = Request.Form["udf6"].Trim();
			string UDF7 = Request.Form["udf7"].Trim();
			string UDF8 = Request.Form["udf8"].Trim();
			string UDF9 = Request.Form["udf9"].Trim();
			string UDF10 = Request.Form["udf10"].Trim();

			string Show_payment_mode = Request.Form["show_payment_mode"].Trim();
						
			string split_payments = Request.Form["split_payments"].Trim();
			string sub_merchant_id = Request.Form["sub_merchant_id"].Trim();

			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("txnid", Txnid);
			dict.Add("key", Key);
			amount = amount;
			dict.Add("amount", amount);
			dict.Add("firstname", firstname.Trim());
			dict.Add("email", email.Trim());
			dict.Add("phone", phone.Trim());
			dict.Add("productinfo", productinfo.Trim());
			dict.Add("surl", surl.Trim());
			dict.Add("furl", furl.Trim());
			dict.Add("udf1", UDF1.Trim());
			dict.Add("udf2", UDF2.Trim());
			dict.Add("udf3", UDF3.Trim());
			dict.Add("udf4", UDF4.Trim());
			dict.Add("udf5", UDF5.Trim());
			dict.Add("udf6", UDF6.Trim());
			dict.Add("udf7", UDF7.Trim());
			dict.Add("udf8", UDF8.Trim());
			dict.Add("udf9", UDF9.Trim());
			dict.Add("udf10", UDF10.Trim());
			
			dict.Add("show_payment_mode", Show_payment_mode.Trim());

			if (split_payments.Length > 0)
			{
				dict.Add("split_payments", split_payments);
			}
			if (sub_merchant_id.Length > 0)
			{
				dict.Add("sub_merchant_id", sub_merchant_id);
			}
			
			Easebuzz t = new Easebuzz(salt, Key, env, "false");
			string result = t.initiatePaymentAPI(dict);

			if (is_enable_iframe == "true") {
				if (((JToken)result).Type == JTokenType.Object)
				{
					Response.Write(result);
				}
				else
                {
					this.accessKey = result;
					ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "processPayment()", true);
				}
				
			}
			else
            {

				bool isUri = Uri.IsWellFormedUriString(result, UriKind.RelativeOrAbsolute);
				if (isUri)
				{
					Response.Write(string.Format("<script type='text/javascript'>window.open('{0}', '_self');</script>", result));
				}
				else
                {
					Response.Write(result);

				}
			}
			
		}

	}
}

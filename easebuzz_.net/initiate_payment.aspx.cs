using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;

namespace easebuzz_.net
{
	public partial class initiate_payment : System.Web.UI.Page
    {
		public string salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
        public string Key = System.Configuration.ConfigurationSettings.AppSettings["key"];

        public string env = "prod";
        
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

			//call the object of class and start payment
			Easebuzz t = new Easebuzz(salt, Key, env);
			string strForm = t.initiatePaymentAPI(amount, firstname, email, phone, productinfo, surl, furl,Txnid,UDF1,UDF2,UDF3,UDF4,UDF5, UDF6, UDF7, UDF8, UDF9, UDF10, Show_payment_mode, split_payments, sub_merchant_id);
            Page.Controls.Add(new LiteralControl(strForm));
        }

	}
}

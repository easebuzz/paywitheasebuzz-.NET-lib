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

	public partial class success : System.Web.UI.Page
	{
		//reverse hash matching while response
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{

				string[] merc_hash_vars_seq;
				string merc_hash_string = string.Empty;
				string merc_hash = string.Empty;
				string order_id = string.Empty;
				string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";


				merc_hash_vars_seq = hash_seq.Split('|');
				Array.Reverse(merc_hash_vars_seq);
				merc_hash_string = System.Configuration.ConfigurationSettings.AppSettings["salt"] + "|" + Request.Form["status"];


				foreach (string merc_hash_var in merc_hash_vars_seq)
				{
					merc_hash_string += "|";
					merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");

				}
				merc_hash = Easebuzz_Generatehash512(merc_hash_string).ToLower();



				if (merc_hash != Request.Form["hash"])
				{
					Response.Write("Hash value did not matched");
				}
				else
				{
					order_id = Request.Form["txnid"];

					//Response.Write("value matched");+		this	{ASP.success_aspx}	easebuzz_.net.success {ASP.success_aspx}

					if (Request.Form["status"] == "success")
					{
						Response.Write(Request.Form);
					}
					else
					{
						Response.Write(Request.Form);
					}
					//Hash value did not matched
				}

			}

			catch (Exception ex)
			{
				Response.Write("<span style='color:red'>" + ex.Message + "</span>");

			}
		}

		// hashcode generation
		public string Easebuzz_Generatehash512(string text)
		{

			byte[] message = Encoding.UTF8.GetBytes(text);

			UnicodeEncoding UE = new UnicodeEncoding();
			byte[] hashValue;
			SHA512Managed hashString = new SHA512Managed();
			string hex = "";
			hashValue = hashString.ComputeHash(message);
			foreach (byte x in hashValue)
			{
				hex += String.Format("{0:x2}", x);
			}
			return hex;

		}

	}
}

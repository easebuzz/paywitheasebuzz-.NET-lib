using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;

public partial class ResponseHandling : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        
        string [] merc_hash_vars_seq ;
        string merc_hash_string = string.Empty;
        string merc_hash = string.Empty;
        string order_id = string.Empty;         
        string hash_seq="key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";

        if (Request.Form["status"]=="success")
        {
        
        merc_hash_vars_seq = hash_seq.Split('|');
           Array.Reverse(merc_hash_vars_seq);
            merc_hash_string = ConfigurationManager.AppSettings["SALT"] + "|" + Request.Form["status"];


            foreach (string merc_hash_var in merc_hash_vars_seq)
            {
			//Response.Write(Request.Form[merc_hash_var]+"<br/>");
            merc_hash_string += "|";
            merc_hash_string = merc_hash_string +  (Request.Form[merc_hash_var]!=null ? Request.Form[merc_hash_var] : "");

            }
			foreach(string key in Request.Form.Keys ) 
			{
			   Response.Write ( key+":"+Request.Form[key]+"<br />" );
			}
            merc_hash = Generatehash512(merc_hash_string).ToLower();



            if (merc_hash!=Request.Form["hash"])
            {
                Response.Write("Hash value did not matched");
                
            }
            else
            {
                order_id = Request.Form["txnid"];
                
                Response.Write("value matched");
               
                //Hash value did not matched
            }

        }
        else
            {

                Response.Write("Hash value did not matched");
           // osc_redirect(osc_href_link(FILENAME_CHECKOUT, 'payment' , 'SSL', null, null,true));
            
            }
        }
        catch( Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");

        }
    }
    public string Generatehash512(string text)
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

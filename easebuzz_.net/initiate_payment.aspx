<%@ Page Language="C#" Inherits="easebuzz_.net.initiate_payment" %>
<!DOCTYPE html>
<html>
<head runat="server">
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="ie=edge">
        <link rel="stylesheet" href="assets/css/style.css">
	<title>initiate_payment</title>

       <script type="text/javascript">

           function processPayment() {
               var access_key = "<%=accessKey%>";
               var merchant_key = "<%=Key%>";
               var env = "<%=env%>";

               //alert(env);
               var easebuzzCheckout = new EasebuzzCheckout(merchant_key, env)

               var options = {
                   access_key: access_key, // access key received via Initiate Payment

                   onResponse: (response) => {
                       console.log(response);
                   },
                   theme: "#123456" // color hex
               }
               easebuzzCheckout.initiatePayment(options);

           }
       </script>
    <script type="text/javascript" src="https://ebz-static.s3.ap-south-1.amazonaws.com/easecheckout/easebuzz-checkout.js"></script>
    <style type="text/css">
        .auto-style1
        {
            width: 93px;
        }
        .auto-style2
        {
            width: 25px;
        }
    </style>
</head>

    <body>
        <div class="grid-container">
            <header class="wrapper">
                <div class="logo">
                    <a href="#">
                        <img src="assets/images/eb-logo.svg" alt="Easebuzz">
                    </a>
                </div>

                <div class="hedding">
                    <h2><a class="highlight" href="../index.html">Back</a></h2>
                </div>
            </header>
            
            <div class="form-container">
                <h2>INITIATE PAYMENT API</h2>
                <hr>
                <form id="form1" runat="server">
                    <div class="main-form">
                        <h3>Mandatory Parameters</h3>
                        <hr>
                        <div class="mandatory-data">
                            <div class="form-field">
                                <label for="txnid">Transaction ID<sup>*</sup></label>
                                <input id="txnid" class="txnid" name="txnid" value="" placeholder="T31Q6JT8HB">
                            </div>

                            <div class="form-field">
                                <label for="amount">Amount<sup>(should be float)*</sup></label>
                                <input id="amount" class="amount" name="amount" value="" placeholder="125.25">
                            </div>  

                            <div class="form-field">
                                <label for="firstname">First Name<sup>*</sup></label>
                                <input id="firstname" class="firstname" name="firstname" value="" placeholder="Easebuzz Pvt. Ltd.">
                            </div>
                    
                            <div class="form-field">
                                <label for="email">Email ID<sup>*</sup></label>
                                <input id="email" class="email" name="email" value=""
                                placeholder="initiate.payment@easebuzz.in">
                            </div>
                    
                            <div class="form-field">
                                <label for="phone">Phone<sup>*</sup></label>
                                <input id="phone" class="phone" name="phone" value=""
                                placeholder="0123456789">
                            </div>
                            
                            <div class="form-field">
                                <label for="productinfo">Product Information<sup>*</sup></label>
                                <input id="productinfo" class="productinfo" name="productinfo" value="" placeholder="Apple Laptop">
                            </div>
                    
                            <div class="form-field">
                                <label for="surl">Success URL<sup>*</sup></label>
                                <input id="surl" class="surl" name="surl" value="http://localhost:8080/success.aspx" placeholder="http://localhost:8080/success.aspx">
                            </div>
                            
                            <div class="form-field">
                                <label for="furl">Failure URL<sup>*</sup></label>
                                <input id="furl" class="furl" name="furl" value="http://localhost:8080/success.aspx"
                                placeholder="http://localhost:8080/success.aspx">
                            </div>

                        </div>

                        <h3>Optional Parameters</h3>
                        <hr>
                        <div class="optional-data">

                            <div class="form-field">
                                <label for="udf1">UDF1</label>
                                <input id="udf1" class="udf1" name="udf1" value="" placeholder="User description1">
                            </div>
                        
                            <div class="form-field">
                                <label for="udf2">UDF2</label>
                                <input id="udf2" class="udf2" name="udf2" value="" placeholder="User description2">
                            </div>
                    
                            <div class="form-field">
                                <label for="udf3">UDF3</label>
                                <input id="udf3" class="udf3" name="udf3" value="" placeholder="User description3">
                            </div>
                    
                            <div class="form-field">
                                <label for="udf4">UDF4</label>
                                <input id="udf4" class="udf4" name="udf4" value="" placeholder="User description4">
                            </div>
                    
                            <div class="form-field">
                                <label for="udf5">UDF5</label>
                                <input id="udf5" class="udf5" name="udf5" value="" placeholder="User description5">
                            </div>
                            
                            
                            <div class="form-field">
                                <label for="udf6">UDF6</label>
                                <input id="udf6" class="udf6" name="udf6" value="" placeholder="User description6">
                            </div>
                            
                            <div class="form-field">
                                <label for="udf7">UDF7</label>
                                <input id="udf7" class="udf7" name="udf7" value="" placeholder="User description7">
                            </div>
                            
                            <div class="form-field">
                                <label for="udf8">UDF8</label>
                                <input id="udf8" class="udf8" name="udf8" value="" placeholder="User description8">
                            </div>
                            
                            <div class="form-field">
                                <label for="udf9">UDF9</label>
                                <input id="udf9" class="udf9" name="udf9" value="" placeholder="User description9">
                            </div>
                            
                            <div class="form-field">
                                <label for="udf10">UDF10</label>
                                <input id="udf10" class="udf10" name="udf10" value="" placeholder="User description10">
                            </div>
                            
                            <div class="form-field">
                                <label for="split_payments">Split Payments</label>
                                 <input id="split_payments" class="split_payments" name="split_payments" value="" placeholder='{ "axisaccount" : 100, "hdfcaccount" : 100}'>
                            </div>

                            <div class="form-field">
                                <label for="address1">Address 1</label>
                                <input id="address1" class="address1" name="address1" value="" 
                                placeholder="#250, Main 5th cross,">
                            </div>
                    
                            <div class="form-field">
                                <label for="address2">Address 2</label>
                                <input id="address2" class="address2" name="address2" value="" 
                                placeholder="Saket nagar, Pune">
                            </div>
                            
                            <div class="form-field">
                                <label for="city">City</label>
                                <input id="city" class="city" name="city" value="" placeholder="Pune">
                            </div>
                    
                            <div class="form-field">
                                <label for="state">State</label>
                                <input id="state" class="state" name="state" value="" placeholder="Maharashtra">
                            </div>
                    
                            <div class="form-field">
                                <label for="country">Country</label>
                                <input id="country" class="country" name="country" value="" placeholder="India">
                            </div>
                            
                            <div class="form-field">
                                <label for="zipcode">Zip-Code</label>
                                <input id="zipcode" class="zipcode" name="zipcode" value="" placeholder="123456">
                            </div>

                              <div class="form-field">
                                <label for="show_payment_mode">Show Payment Mode</label>
                                <input id="show_payment_mode" class="show_payment_mode" name="show_payment_mode" value="" placeholder="NB,DC,CC,DAP,MW,UPI,OM,EMI">
                            </div>

                             <div class="form-field">
                                <label for="sub_merchant_id">Sub Merchant Id</label>
                                <input id="sub_merchant_id" class="sub_merchant_id" name="sub_merchant_id" value="" placeholder="Sub Merchant Id">
                            </div>

                        </div>
                
                        <input type="hidden" name="initiatePaymentAPI" value="initiatePaymentAPI">
                        <div class="btn-submit">
                            <asp:Button id="button1" runat="server" Text="initiate payment" OnClick="button1Clicked" />
        
    </style>
                        </div>
                    </div>
                </form>
            </div>
            
        </div>
    </body>

</html>

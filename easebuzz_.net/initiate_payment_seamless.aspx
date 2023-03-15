<%@ Page Language="C#" Inherits="easebuzz_.net.initiate_payment_seamless" %>
<!DOCTYPE html>
<html>
<head runat="server">
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="ie=edge">
        <link rel="stylesheet" href="assets/css/style.css">
	<title>initiate_payment_seamless</title>

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
                <h2>INITIATE SEAMLESS PAYMENT API</h2>
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
                                <label for="split_payments">Split Payments</label>
                                 <input id="split_payments" class="split_payments" name="split_payments" value="" placeholder='{ "axisaccount" : 100, "hdfcaccount" : 100}'>
                            </div>

                             <div class="form-field">
                                <label for="sub_merchant_id">Sub Merchant Id</label>
                                <input id="sub_merchant_id" class="sub_merchant_id" name="sub_merchant_id" value="" placeholder="Sub Merchant Id">
                            </div>

                             <div class="form-field">
                                <label for="payment_mode">Payment Mode</label>
                                <input id="payment_mode" class="payment_mode" name="payment_mode" value="" placeholder="Write payment mode">
                            </div>

                            <div class="form-field">
                                <label for="bank_code">Bank Code</label>
                                <input id="bank_code" class="bank_code" name="bank_code" value="" placeholder="Write bank code provided by Easebuzz">
                            </div>

                            <div class="form-field">
                                <label for="card_number">Card Number</label>
                                <input id="card_number" class="card_number" name="card_number" value="" placeholder="Write Card Number if payment mode is Debit Card/Credit Card">
                            </div>

                            <div class="form-field">
                                <label for="card_holder_name">Card Holder Name</label>
                                <input id="card_holder_name" class="card_holder_name" name="card_holder_name" value="" placeholder="Write Card Holder Name if payment mode is Debit Card/Credit Card">
                            </div>

                            <div class="form-field">
                                <label for="card_cvv">Card CVV</label>
                                <input id="card_cvv" class="card_cvv" name="card_cvv" value="" placeholder="Write Card CVV if payment mode is Debit Card/Credit Card">
                            </div>
                            
                            <div class="form-field">
                                <label for="card_expiry_date">Card Expiry Date</label>
                                <input id="card_expiry_date" class="card_expiry_date" name="card_expiry_date" value="" placeholder="Write Card Expiry Date if payment mode is Debit Card/Credit Card">
                            </div>

                              <div class="form-field">
                                <label for="upi_va">UPI VA</label>
                                <input id="upi_va" class="upi_va" name="upi_va" value="" placeholder="Write UPI VA">
                            </div>
                            <div class="form-field">
                                <label for="transaction_mode">transaction_mode</label>
                                <input id="transaction_mode" class="transaction_mode" name="transaction_mode" value="seamless_vpa" placeholder="Transaction Mode">
                            </div>

                            <!-- 
                            <div class="form-field">
                                <label for="card_token">Card Token</label>
                                <input id="card_token" class="card_token" name="card_token" value="" placeholder="Enter token of card">
                            </div>

                            <div class="form-field">
                                <label for="cryptogram">Card Cryptogram</label>
                                <input id="cryptogram" class="cryptogram" name="cryptogram" value="" placeholder="Cryptogram of card">
                            </div>

                            <div class="form-field">
                                <label for="token_expiry_date">Token Expiry Date</label>
                                <input id="token_expiry_date" class="token_expiry_date" name="token_expiry_date" value="" placeholder="Write Token Expiry Date if payment mode is Debit Card/Credit Card">
                            </div>

                            <div class="form-field">
                                <label for="token_requester_id">Token Requester ID</label>
                                <input id="token_requester_id" class="token_requester_id" name="token_requester_id" value="EBZCBS456" placeholder="Write Token Requester id">
                            </div> 
                             -->
            
                        </div>
                
                        <input type="hidden" name="initiatePaymentAPI" value="initiatePaymentAPI">
                        <div class="btn-submit">
                            <asp:Button id="button1" runat="server" Text="Initiate Seamless Payment" OnClick="button1Clicked" />
        
    </style>
                        </div>
                    </div>
                </form>
            </div>
            
        </div>
    </body>

</html>

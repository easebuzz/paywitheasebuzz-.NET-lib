<%@ Page Language="C#" Inherits="easebuzz_.net.Default" %>

<!DOCTYPE html>
<html lang="en">
    <head runat="server">
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="ie=edge">
        <link rel="stylesheet" href="assets/css/style.css">
        <title>Easebuzz Payment Gateway Kit</title>
    </head>
    <body>
        <div class="grid-container">
            <header class="wrapper">
                <div class="logo">
                    <a href="index.html">
                        <img src="assets/images/eb-logo.svg" alt="Easebuzz">
                    </a>
                </div>

                <div class="hedding">
                    <a href="https://docs.easebuzz.in/">
                        <h2><spam>Easebuzz Documentation</spam></h2>
                    </a>
                </div>
            </header>

            <div class="container">
                <div class="card-container">
                    <div class="card">
                        <h2>Easebuzz-lib For .Net</h2>
                        <hr>
                        <p>There are five Easebuzz Payment Gateway API</p>
                        <ul class="tabs">
                            <li id="initiate_payment_tab">
                                <a href="initiate_payment.aspx">Initiate Payment API</a>
                            </li>
                            <li id="transaction_tab">
                                <a href="transactional_data.aspx">Transaction API</a>
                            </li>
                            <li id="transaction_date_tab">
                                <a href="transaction_byDate.aspx">Transaction Date API</a>
                            </li>
                            <li id="refund_tab">
                                <a href="refund.aspx">Refund API</a>
                            </li>
                            <li id="payout_tab">
                                <a href="payout.aspx">Payout API</a>
                            </li>
                        </ul>
                    </div>   
                </div>

            </div>

        </div>
   
        
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script> 
       
    </body>
</html>



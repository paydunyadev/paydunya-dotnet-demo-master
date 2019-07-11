using System;
using Paydunya;

namespace PaydunyaTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Generate API keys from Paydunya and setup
            //          PaydunyaSetup setup = new PaydunyaSetup {
            //              MasterKey = "wQzk9ZwR-Qq9m-0hD0-zpud-je5coGC3FHKW",
            //              PrivateKey = "test_private_rMIdJM3PLLhLjyArx9tF3VURAF5",
            //              PublicKey = "test_public_kb9Wo0Qpn8vNWMvMZOwwpvuTUja",
            //              Token = "IivOiOxGJuWhc5znlIiK",
            //              Mode = "test"
            //          };

            PaydunyaSetup setup = new PaydunyaSetup
            {
                MasterKey = "wQzk9ZwR-Qq9m-0hD0-zpud-je5coGC3FHKW",
                PrivateKey = "live_private_webVPJoCX15O3A6Z0m5d4VyZMwk",
                PublicKey = "live_public_ObzE0-VBYrIsaJtkKg5h9tp6_KY",
                Token = "97fc2a44817a2aab059c",
                Mode = "live"
            };

            PaydunyaStore store = new PaydunyaStore
            {
                Name = "Magasin Chez Sandra",
                Tagline = "L'elegance n'a pas de prix",
                PhoneNumber = "336530583",
                PostalAddress = "Dakar Plateau - Etablissement kheweul",
                LogoUrl = "http://www.chez-sandra.sn/logo.png",
                //              CancelUrl = "http://www.google.com",
                //              ReturnUrl = "http://www.yahoo.com/"
            };

            //          Performing an Paydunya Checkout Request
            PaydunyaCheckoutInvoice checkout_invoice = new PaydunyaCheckoutInvoice(setup, store);
            checkout_invoice.AddItem("Clavier DELL", 2, 3000, 6000);
            checkout_invoice.AddItem("Ordinateur Lenovo L440", 1, 400000, 400000, "Description optionelle");
            checkout_invoice.AddItem("Casque Logitech", 1, 8000, 8000);

            checkout_invoice.SetTotalAmount(414000);
            checkout_invoice.SetCustomData("Name", "Alioune Badara");
            checkout_invoice.AddTax("VAT (18%)", 74520);
            checkout_invoice.AddTax("Autre taxe (15%)", 62100);

            Console.WriteLine("Paydunya Checkout Request Test");

            if (checkout_invoice.Create())
            {
                Console.WriteLine(checkout_invoice.Token);
                Console.WriteLine(checkout_invoice.Status);
                Console.WriteLine(checkout_invoice.ResponseText);
                Console.WriteLine(checkout_invoice.GetInvoiceUrl());
            }
            else
            {
                Console.WriteLine(checkout_invoice.ResponseText);
                Console.WriteLine(checkout_invoice.Status);
            }


            //          Performing an Paydunya PSR Request & Charge

            PaydunyaOnsiteInvoice psr_invoice = new PaydunyaOnsiteInvoice(setup, store);
            psr_invoice.AddItem("Clavier DELL", 2, 3000, 6000);
            psr_invoice.AddItem("Ordinateur Lenovo L440", 1, 400000, 400000, "Description optionelle");
            psr_invoice.AddItem("Casque Logitech", 1, 8000, 8000);

            psr_invoice.SetTotalAmount(414000);
            psr_invoice.SetCustomData("Name", "Alioune Badara");
            psr_invoice.AddTax("VAT (18%)", 74520);
            psr_invoice.AddTax("Autre taxe (15%)", 62100);

            //          The Request
            Console.WriteLine("PAYDUNYA PSR Request Test");

            if (psr_invoice.Create("EMAIL_OU_NUMERO_MOBILE_DU_CLIENT_PAYDUNYA"))
            {
                Console.WriteLine(psr_invoice.Token);
                Console.WriteLine(psr_invoice.Status);
                Console.WriteLine(psr_invoice.ResponseText);
            }
            else
            {
                Console.WriteLine(psr_invoice.ResponseText);
                Console.WriteLine(psr_invoice.Status);
            }

            //          The Charge
            Console.WriteLine("PAYDUNYA PSR Charge Test");
            PaydunyaOnsiteInvoice opr_invoice2 = new PaydunyaOnsiteInvoice(setup, store);
            if (opr_invoice2.Charge("PSR-cd28584202305f04", "XYDQLZ"))
            {
                Console.WriteLine(opr_invoice2.Status);
                Console.WriteLine(opr_invoice2.ResponseText);
                Console.WriteLine(opr_invoice2.GetReceiptUrl());
            }
            else
            {
                Console.WriteLine(opr_invoice2.Status);
                Console.WriteLine(opr_invoice2.ResponseText);
            }

            //          Paying an Paydunya Account holder using PaydunyaDirectPay
            Console.WriteLine("PAYDUNYA TFA Test");
            PaydunyaDirectPay direct_pay = new PaydunyaDirectPay(setup);
            if (direct_pay.CreditAccount("EMAIL_OU_NUMERO_MOBILE_DU_CLIENT_PAYDUNYA", 5000))
            {
                Console.WriteLine(direct_pay.Description);
                Console.WriteLine(direct_pay.Status);
                Console.WriteLine(direct_pay.ResponseText);
            }
            else
            {
                Console.WriteLine(direct_pay.ResponseText);
                Console.WriteLine(direct_pay.Status);
            }
        }
    }
}

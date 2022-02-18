using System;
using PayPalCheckout.Controllers;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter clienId:");
            var clientId = Console.ReadLine();

            Console.Write("Enter SecretKey:");
            var secret = Console.ReadLine();


            var paypal = new PayPal(clientId,secret, "Sandbox");
            paypal.CreatePayment();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}

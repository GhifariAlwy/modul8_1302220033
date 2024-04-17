// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

class BankTransferConfig
{
    public string Lang { get; set; }
    public TransferConfig Transfer { get; set; }
    public List<string> Methods { get; set; }
    public ConfirmationConfig Confirmation { get; set; }

    public BankTransferConfig()
    {
       
        string configFile = "bank_transfer_config.json";
        if (File.Exists(configFile))
        {
            string jsonData = File.ReadAllText(configFile);
            var config = JsonConvert.DeserializeObject<BankTransferConfig>(jsonData);
            Lang = config.Lang;
            Transfer = config.Transfer;
            Methods = config.Methods;
            Confirmation = config.Confirmation;
        }
        else
        {
            Lang = "en";
            Transfer = new TransferConfig
            {
                Threshold = 25000000,
                LowFee = 6500,
                HighFee = 15000
            };
            Methods = new List<string> { "RTO (real-time)", "SKN", "RTGS", "BI FAST" };
            Confirmation = new ConfirmationConfig
            {
                En = "yes",
                Id = "ya"
            };
        }
    }
}

class TransferConfig
{
    public int Threshold { get; set; }
    public int LowFee { get; set; }
    public int HighFee { get; set; }
}

class ConfirmationConfig
{
    public string En { get; set; }
    public string Id { get; set; }
}

class Program
{
    static void Main()
    {
        BankTransferConfig config = new BankTransferConfig();
        if (config.Lang == "en")
        {
            Console.WriteLine("Please insert the amount of money to transfer:");
        }
        else
        {
            Console.WriteLine("Masukkan jumlah uang yang akan di-transfer:");
        }
        int nominalTransfer = int.Parse(Console.ReadLine());
        int threshold = config.Transfer.Threshold;
        int lowFee = config.Transfer.LowFee;
        int highFee = config.Transfer.HighFee;

        int transferFee;
        if (nominalTransfer <= threshold)
        {
            transferFee = lowFee;
        }
        else
        {
            transferFee = highFee;
        }
        int totalAmount = nominalTransfer + transferFee;
        if (config.Lang == "en")
        {
            Console.WriteLine($"Transfer fee = {transferFee}");
            Console.WriteLine($"Total amount = {totalAmount}");
        }
        else
        {
            Console.WriteLine($"Biaya transfer = {transferFee}");
            Console.WriteLine($"Total biaya = {totalAmount}");
        }
        if (config.Lang == "en")
        {
            Console.WriteLine("Select transfer method:");
        }
        else
        {
            Console.WriteLine("Pilih metode transfer:");
        }
        for (int i = 0; i < config.Methods.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {config.Methods[i]}");
        }

        string methodChoice = Console.ReadLine();

        if (config.Lang == "en")
        {
            Console.WriteLine($"Please type '{config.Confirmation.En}' to confirm the transaction:");
        }
        else
        {
            Console.WriteLine($"Ketik '{config.Confirmation.Id}' untuk mengkonfirmasi transaksi:");
        }

        string confirmationInput = Console.ReadLine();

        bool isConfirmationMatch = false;
        if (config.Lang == "en")
        {
            isConfirmationMatch = confirmationInput == config.Confirmation.En;
        }
        else if (config.Lang == "id")
        {
            isConfirmationMatch = confirmationInput == config.Confirmation.Id;
        }

        if (isConfirmationMatch)
        {
            if (config.Lang == "en")
            {
                Console.WriteLine("The transfer is completed");
            }
            else
            {
                Console.WriteLine("Proses transfer berhasil");
            }
        }
        else
        {
            if (config.Lang == "en")
            {
                Console.WriteLine("Transfer is cancelled");
            }
            else
            {
                Console.WriteLine("Transfer dibatalkan");
            }
        }
    }
}

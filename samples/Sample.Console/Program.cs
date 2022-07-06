﻿using NTwain;
using NTwain.Data;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Sample
{
    class Program
    {
        const string PAPERSTREAM = "PaperStream";
        static readonly TwainSession twain = InitTwain();

        private static TwainSession InitTwain()
        {
            var twain = new TwainSession(TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetExecutingAssembly()));

            twain.TransferReady += (s, e) =>
            {
                Console.WriteLine("Got xfer ready on thread {0}.", Thread.CurrentThread.ManagedThreadId);
            };

            twain.DataTransferred += (s, e) =>
            {
                if (e.NativeData != IntPtr.Zero)
                    Console.WriteLine("SUCCESS! Got twain data on thread {0}.", Thread.CurrentThread.ManagedThreadId);
                else
                    Console.WriteLine("BUMMER! No twain data on thread {0}.", Thread.CurrentThread.ManagedThreadId);
            };

            twain.SourceDisabled += (s, e) =>
            {
                Console.WriteLine("Source disabled on thread {0}.", Thread.CurrentThread.ManagedThreadId);
                var rc = twain.CurrentSource.Close();
                rc = twain.Close();
            };

            return twain;
        }

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (PlatformInfo.Current.IsApp64Bit)
                Console.WriteLine("[64bit]");
            else
                Console.WriteLine("[32bit]");

            // just an amusing example to do twain in console without UI
            ThreadPool.QueueUserWorkItem(o =>
            {
                try { DoTwainWork(); }
                catch (Exception ex) { Console.WriteLine("ERROR: " + ex.ToString()); }
            });
            Console.WriteLine("Test started, press Enter to exit.");
            Console.ReadLine();
        }

        /// <summary>
        /// TwainWork
        /// </summary>
        static void DoTwainWork()
        {
            Console.WriteLine("Getting ready to do twain stuff on thread {0}...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);

            var rc = twain.Open();

            if (rc == ReturnCode.Success)
            {                
                var hit = twain.FirstOrDefault(s => s.Name.Contains(PAPERSTREAM));
                if (hit == null)
                {
                    Console.WriteLine("The sample source \"" + PAPERSTREAM + "\" is not installed.");
                    twain.Close();
                }
                else
                {
                    rc = hit.Open();

                    if (rc == ReturnCode.Success)
                    {
                        Console.WriteLine("Starting capture from the sample source...");
                        rc = hit.Enable(SourceEnableMode.NoUI, false, IntPtr.Zero);
                    }
                    else
                    {
                        Console.WriteLine("The sample source \"" + PAPERSTREAM + "\" isn't opened.");
                        twain.Close();
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to open dsm with rc={0}!", rc);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Argix.Enterprise;

namespace Argix.IT {
    //
    class Program {
        //Members

        //Interface
        static void Main(string[] args) {
            //
            try {
                Console.WriteLine("Please enter a notice to publish:");
                string notice = Console.ReadLine();
                MembershipDataset users = new MembershipGateway().GetUsers();
                for (int i = 0;i < users.UserTable.Count;i++) {
                    try {
                        if (users.UserTable[i].UserName == "jheary") {
                            bool sent = new SMTPGateway().SendNotice(notice,"Argix Logistics Important Notice",users.UserTable[i].Email);
                            Console.WriteLine("SEND: " + users.UserTable[i].UserName + " at " + users.UserTable[i].Email);
                        }
                        else
                            Console.WriteLine("NO SEND: " + users.UserTable[i].UserName + " at " + users.UserTable[i].Email);
                    }
                    catch (Exception exx) { Console.WriteLine(exx.Message); }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            string input = Console.ReadLine();
        }
    }
}

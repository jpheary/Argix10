using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Argix {
    class Program {
        static void Main(string[] args) {
            //
            Console.WriteLine(FinanceGateway.GetMileage("75006", "77027").ToString());
            Console.WriteLine(FinanceGateway.GetMileage("28217", "29910").ToString());
            Console.WriteLine(FinanceGateway.GetMileage("8831", "98105").ToString());
            Console.WriteLine(FinanceGateway.GetMileage("8831", "37402").ToString());
            Console.WriteLine(FinanceGateway.GetMileage("8831", "84098").ToString());
            Console.WriteLine(FinanceGateway.GetMileage("8831", "43240").ToString());
            Console.WriteLine(FinanceGateway.GetMileage("90760", "92230").ToString());
            Console.WriteLine(FinanceGateway.GetMileage("33811", "34243").ToString());
            Console.WriteLine(FinanceGateway.GetMileage("7657", "6498").ToString());
            Console.WriteLine(FinanceGateway.GetMileage("1887", "2043").ToString());
            Console.ReadLine();
        }
    }
}

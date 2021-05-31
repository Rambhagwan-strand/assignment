using System;
using System.Collections.Generic;

namespace HelloWorld
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Member> Person = new List<Member>();
            Console.WriteLine("Please enter number of partners");
            int total = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Equal contribution (type Yes or No)");
            string IfEqualContribution = Console.ReadLine();
            bool EqualSharing = false;
            if (IfEqualContribution[0] == 'y' || IfEqualContribution[0] == 'Y')
            {
                EqualSharing = true;
            }
            
            //Console.WriteLine(total);
            for (int i = 0; i < total; i++)
            {
                
                Console.WriteLine(EqualSharing);
                Console.WriteLine("Enter Customer Name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter spend amount by {0}", name);
                int moneySpend = Convert.ToInt32(Console.ReadLine());
                double share = 1;
                if (EqualSharing == false)
                {
                    Console.WriteLine("Type sharing ratio either in numbers. You may put percentages also");
                    share = Convert.ToDouble(Console.ReadLine());
                }
                Member m1 = new Member(name, moneySpend, share);
                Person.Add(m1);

            }
            PrintShare(Person);


            Console.ReadKey();

        }

        private static void PrintShare(List<Member> Person)
        {
            double SumOfRatio = 0;
            double totalMoney = 0;
            double MoneyAdjustmentAfterSettlement = 0;
            string Dialogue;
            for (int i = 0; i < Person.Count; i++)
            {
                SumOfRatio += Person[i].share;
                totalMoney += Person[i].moneyGiven;
            }
            if (SumOfRatio == 0)
            {
                Console.WriteLine("Total sum of share can not be zero");
            } else
            {
                for (int i = 0; i < Person.Count; i++)
                {
                    MoneyAdjustmentAfterSettlement = Person[i].moneyGiven - (double)(Person[i].share / SumOfRatio) * totalMoney ;
                    if (MoneyAdjustmentAfterSettlement >= 0)
                    {
                        Dialogue = "need to receive";
                    }
                    else
                    {
                        Dialogue = "need to pay";
                    }
                    Console.WriteLine("{0} {1} {2}", Person[i].name, Dialogue, Math.Abs(MoneyAdjustmentAfterSettlement));
                    Console.WriteLine("____________________________________________________");

                }
                    
            }

        }
    }
}

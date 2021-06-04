using System;
using System.Collections.Generic;
using System.IO;

namespace SplitWise
{
    class Program
    {

        static void Main(string[] args)
        {
            //opening tsv file by giving path to tsv file. 
            string[] tsvfile = File.ReadAllLines(@"C:\Users\User\Desktop\New folder\Sample2.tsv");
            List<Member> Persons = new List<Member>();
            OutputFinalSettlementForAllGroup(tsvfile, Persons);

            Console.ReadKey();

        }

        private static void OutputFinalSettlementForAllGroup(string[] tsvfile, List<Member> Persons)
        {
            for (int rowNumber = 1; rowNumber < tsvfile.Length; rowNumber++)
            {
                string[] rowData = tsvfile[rowNumber].Split('\t');

                //Group ended because there is an empty line in tsv file.
                //So we will calculate for current group till now and create new group further
                if (rowData[0].Length == 0)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Group Calculation");
                    PrintSettledShareForOneGroup(Persons);  //calculate for current group
                    Persons.Clear(); //clearing existing group
                    continue;
                }

                //if share value is not given. Then we are assuming equal share.
                if (rowData[2].Length == 0)
                {
                    rowData[2] = "1";
                }
                Member member = new Member(rowData[0], Convert.ToInt32(rowData[1]), Convert.ToDouble(rowData[2]));
                Persons.Add(member);
            }


            Console.WriteLine(" ");
            Console.WriteLine("Group Calculation");
            PrintSettledShareForOneGroup(Persons);
        }

        public static void PrintSettledShareForOneGroup(List<Member> Persons)
        {
            //No person is there so no settlement required.
            if (Persons.Count == 0)
            {
                return;
            }

            double SumOfRatio = 0;
            double totalMoney = 0;
            double MoneyAdjustmentAfterSettlement = 0;
            double ExpectedFromCurrentPerson;
            string Dialogue;
            for (int i = 0; i < Persons.Count; i++)
            {
                SumOfRatio += Persons[i].share;
                totalMoney += Persons[i].moneyGiven;
            }
            if (SumOfRatio == 0)
            {
                Console.WriteLine("Total sum of share can not be zero");
            } else
            {
                for (int i = 0; i < Persons.Count; i++)
                {

                    ExpectedFromCurrentPerson = (double)(Persons[i].share / SumOfRatio) * totalMoney;
                    MoneyAdjustmentAfterSettlement = Math.Round(Persons[i].moneyGiven - ExpectedFromCurrentPerson, 2);
                    if (MoneyAdjustmentAfterSettlement >= 0)
                    {
                        Dialogue = "need to receive";
                    }
                    else
                    {
                        Dialogue = "need to pay";
                    }
                    Console.WriteLine("{0} {1} {2}", Persons[i].name, Dialogue, Math.Abs(MoneyAdjustmentAfterSettlement));
                    Console.WriteLine("____________________________________________________");

                }
                Console.WriteLine("***************************************************");
                    
            }

        }
    }
}

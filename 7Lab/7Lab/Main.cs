
Console.WriteLine("Hello, World!");
namespace _7Lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            test1();
            test2();
            test3();
        }

        public static void test1()
        {
            List<Approval> approvals = new List<Approval>();

            approvals.Add(new Approval("Сократ", "Человек"));
            approvals.Add(new Approval("Человек", "Живое существо"));
            approvals.Add(new Approval("Живое существо", "Смертно"));
            Resolution resolution = new Resolution(approvals);
            Console.WriteLine(resolution.Method(new Approval("Сократ", "Смертно")));
            Console.ReadKey();
        }

        public static void test2()
        {
            List<Approval> approvals = new List<Approval>();

            approvals.Add(new Approval("Сократ", "Человек"));
            approvals.Add(new Approval("Человек", "Живое существо"));
            approvals.Add(new Approval("Живое существо", "Смертно"));
            Resolution resolution = new Resolution(approvals);
            Console.WriteLine(resolution.Method(new Approval("Сократ", "Не Смертно")));
            Console.ReadKey();

        }

        public static void test3()
        {
            List<Approval> approvals = new List<Approval>();

            approvals.Add(new Approval("Достигает 9 футов роста", "Страус"));
            approvals.Add(new Approval("Птица в этом птичнике", "Птица, принадлежащая мне"));
            approvals.Add(new Approval("Страус", "Не Питается пирогами с начинкой"));
            approvals.Add(new Approval("Птица, принадлежащая мне", "Достигает 9 футов роста"));

            Resolution resolution = new Resolution(approvals);
            Console.WriteLine(resolution.Method(new Approval("Птица, принадлежащая мне", "Не Питается пирогами с начинкой")));
            Console.ReadKey();

        }
    }
}
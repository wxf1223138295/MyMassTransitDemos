using System;
using System.IO.Compression;

namespace ConsoleApp1
{
    class Program
    {
        public delegate string AsyncMethodCaller(int callDuration);
        static void Main(string[] args)
        {
            AsyncMethodCaller caller=new AsyncMethodCaller(test);
            
           var result= caller.BeginInvoke(2,new AsyncCallback(fun), null);

           caller.EndInvoke(result);

           Console.ReadKey();
        }

        public static string test(int callDuration)
        {
            Console.WriteLine($"方法test执行{callDuration}");
            return "www";
        }

        public static void fun(IAsyncResult asyncResult)
        {
            Console.WriteLine("sssssss");
        }
    }
}
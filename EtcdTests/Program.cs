using EtcdGrcpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EtcdTests
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Console.WriteLine("开始分库计算");
            int i = 1;

            //var db = (9527 % 20);
            //while (true)
            //{
            //    if (i == 0) continue;
            //    if ((9527 % i) == db)
            //    {
            //        Console.WriteLine("数据库(" + i + "):" + (9527 % i));
            //    }
            //    i++;
            //    //if (i % 10 == 0)
            //    //{
            //    //    Console.WriteLine("按任意键继续");
            //    //    Console.ReadLine();
            //    //}
            //}


            //var db = (9527 / 20) % 1;
            //while (true)
            //{
            //    var s = (9527 / 20) % i;
            //    if (s==db)
            //    {
            //        Console.WriteLine("数据库(" + i + "):" + s);
            //    }
            //    i++;
            //    //if (i % 10 == 0)
            //    //{
            //    //    Console.WriteLine("按任意键继续");
            //    //    Console.ReadLine();
            //    //}
            //}


            //dic.Add("1",new List<string> { "a"});
            //dic.Add("2", new List<string> { "b" });



            //List<string> list = dic["1"];

            //Console.WriteLine(list[0]);

            //dic["1"][0] = "C";

            //Console.WriteLine(list[0]);



            //dic["1"][0] = "C";

            //Console.WriteLine(list[0]);

            //dic["1"][0] = "K";

            //Console.WriteLine(list[0]);



            //Console.WriteLine("除数:" + (9527 / 10));

            //Console.WriteLine("数据库:" + (9527 / 10) % 8 + 1);
            //Console.WriteLine("表编号:" + (9527 % 10));

            //Console.WriteLine("数据库:" + (((9527 / 10) % 16 + 1) -1)% 2 +1);
            //Console.WriteLine("表编号:" + (9527 % 80));

            //int isTaked = 0;
            //if (Interlocked.Exchange(ref isTaked, 1) != 0)
            //{

            //    Console.Write("cc");
            //};
            //Interlocked.Exchange(ref isTaked, 0);

            EtcdClient etcdClient = new EtcdClient(new Uri("http://127.0.0.1:2379"));
            var s = etcdClient.Get("/111ddd").Result;
            Console.WriteLine(s);



            //var sk = etcdClient.GetRange("/111").Result;
            //var k = etcdClient.Put("/conf/order/99999", "999").Result;

            //var b = etcdClient.Put("/conf/order/redis:add", "66666").Result;
            //var etcdw = etcdClient.WatchRange("/111").Result;
            //etcdw.Subscribe(x =>
            //{
            //    Console.WriteLine("触发啦");
            //});
            //etcdClient.Put("/111", "999");
            //etcdClient.Put("/111", "444");
            //etcdClient.Put("/111", "333");
          
            Console.Read();
        }

        static Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();



        static void GetValue(ref string s)
        {
            s = dic["1"][0];
        }

    }
}

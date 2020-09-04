using System;
using System.IO;
using System.Threading;

namespace Api.Repository
{
    public class LogUtil
    {

        #region 错误信息写入日志

        public static void WriteExpLog(string expMessage, string stackTrace = "")
        {
            //Thread.Sleep(10000);
            string input = "【服务执行错误】\r\n" + expMessage +
                           "\r\n堆栈信息:\r\n" + stackTrace;

            //定义文件信息对象  
            FileInfo finfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "temp\\" + "ExcptionLog.txt");

            /*
             * 这里没必要判断，下边使用了openorcreate自己会创建的
            //文件不存在时创建
            //if (!finfo.Exists)
            //{
            //    FileStream fs = finfo.Create();
            //    fs.Close();
            //}
            */

            /*
             * 这个地方进行文件删除同样会有并发问题，改为下面以filemode的方式进行文件截断
            //判断文件是否存在以及是否大于2K  
            //if (finfo.Exists && finfo.Length > 204800)
            //{
            //    //删除该文件 
            //    finfo.Delete();
            //}
            */

            var fileMode = FileMode.OpenOrCreate;
            //20000KB截断为0
            if (finfo.Exists && finfo.Length > 20480000)
            {
                fileMode = FileMode.Truncate;
            }

            //以共享写的方式创建文件写入流，防止并发报错，它会自动进行线程排队，上一个线程写完了下一个线程才会进行写入
            //但是效率可能有点低，暂时业务上估计没那么大并发， 以后再说
            using (FileStream fs = new FileStream(finfo.FullName, fileMode, FileAccess.Write, FileShare.Write))
            //创建只写文件流  
            //using (FileStream fs = finfo.OpenWrite())//这种写法在并发的情况下会报文件被占用的错误，改用上面的写法即可
            {
                //根据上面创建的文件流创建写数据流  
                StreamWriter w = new StreamWriter(fs);
                try
                {
                    //设置写数据流的起始位置为文件流的末尾  
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    //写入当前系统时间并换行  
                    w.Write("{0:yyyy-MM-dd HH:mm:ss:fff}\r\n", DateTime.Now);
                    //写入日志内容并换行  
                    w.Write(input + "\r\n");
                    //写入----------------并换行  
                    w.Write("---------------------\r\n");
                    //清空缓冲区内容，并把缓冲区内容写入基础流  
                    w.Flush();
                }
                finally
                {
                    //关闭写数据流  
                    w.Close();
                    fs.Close();
                }
            }
        }


        public static void WriteExceptionLog(string expMessage, Exception ex = null)
        {
            //Thread.Sleep(10000);
            string input = "【服务执行错误】\r\n" + expMessage +
                           "\r\n堆栈信息:\r\n" + ex.StackTrace;

            //定义文件信息对象  
            FileInfo finfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "temp\\" + "ExcptionLog.txt");

            /*
             * 这里没必要判断，下边使用了openorcreate自己会创建的
            //文件不存在时创建
            //if (!finfo.Exists)
            //{
            //    FileStream fs = finfo.Create();
            //    fs.Close();
            //}
            */

            /*
             * 这个地方进行文件删除同样会有并发问题，改为下面以filemode的方式进行文件截断
            //判断文件是否存在以及是否大于2K  
            //if (finfo.Exists && finfo.Length > 204800)
            //{
            //    //删除该文件 
            //    finfo.Delete();
            //}
            */

            var fileMode = FileMode.OpenOrCreate;
            //20000KB截断为0
            if (finfo.Exists && finfo.Length > 20480000)
            {
                fileMode = FileMode.Truncate;
            }

            //以共享写的方式创建文件写入流，防止并发报错，它会自动进行线程排队，上一个线程写完了下一个线程才会进行写入
            //但是效率可能有点低，暂时业务上估计没那么大并发， 以后再说
            using (FileStream fs = new FileStream(finfo.FullName, fileMode, FileAccess.Write, FileShare.Write))
            //创建只写文件流  
            //using (FileStream fs = finfo.OpenWrite())//这种写法在并发的情况下会报文件被占用的错误，改用上面的写法即可
            {
                //根据上面创建的文件流创建写数据流  
                StreamWriter w = new StreamWriter(fs);
                try
                {
                    //设置写数据流的起始位置为文件流的末尾  
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    //写入当前系统时间并换行  
                    w.Write("{0:yyyy-MM-dd HH:mm:ss:fff}\r\n", DateTime.Now);
                    //写入日志内容并换行  
                    w.Write(input + "\r\n");
                    //写入----------------并换行  
                    w.Write("---------------------\r\n");
                    //清空缓冲区内容，并把缓冲区内容写入基础流  
                    w.Flush();
                }
                finally
                {
                    //关闭写数据流  
                    w.Close();
                    fs.Close();
                }
            }
        }
        public static void WriteLog(string message)
        {
            //Thread.Sleep(10000);
            string input = "【日志】\r\n" + message;

            //定义文件信息对象  
            FileInfo finfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "message.txt");

            /*
             * 这里没必要判断，下边使用了openorcreate自己会创建的
            //文件不存在时创建
            //if (!finfo.Exists)
            //{
            //    FileStream fs = finfo.Create();
            //    fs.Close();
            //}
            */

            /*
             * 这个地方进行文件删除同样会有并发问题，改为下面以filemode的方式进行文件截断
            //判断文件是否存在以及是否大于2K  
            //if (finfo.Exists && finfo.Length > 204800)
            //{
            //    //删除该文件 
            //    finfo.Delete();
            //}
            */

            var fileMode = FileMode.OpenOrCreate;
            //20000KB截断为0
            if (finfo.Exists && finfo.Length > 20480000)
            {
                fileMode = FileMode.Truncate;
            }

            //以共享写的方式创建文件写入流，防止并发报错，它会自动进行线程排队，上一个线程写完了下一个线程才会进行写入
            //但是效率可能有点低，暂时业务上估计没那么大并发， 以后再说
            using (FileStream fs = new FileStream(finfo.FullName, fileMode, FileAccess.Write, FileShare.Write))
            //创建只写文件流  
            //using (FileStream fs = finfo.OpenWrite())//这种写法在并发的情况下会报文件被占用的错误，改用上面的写法即可
            {
                //根据上面创建的文件流创建写数据流  
                StreamWriter w = new StreamWriter(fs);
                try
                {
                    //设置写数据流的起始位置为文件流的末尾  
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    //写入当前系统时间并换行  
                    w.Write("{0:yyyy-MM-dd HH:mm:ss:fff}\r\n", DateTime.Now);
                    //写入日志内容并换行  
                    w.Write(input + "\r\n");
                    //写入----------------并换行  
                    w.Write("---------------------\r\n");
                    //清空缓冲区内容，并把缓冲区内容写入基础流  
                    w.Flush();
                }
                finally
                {
                    //关闭写数据流  
                    w.Close();
                    fs.Close();
                }
            }
        }
        public static void WriteJCLog(string message)
        {
            //Thread.Sleep(10000);
            string input = "【日志】\r\n" + message;

            //定义文件信息对象  
            FileInfo finfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "temp\\" + "JCMessage.txt");

            /*
             * 这里没必要判断，下边使用了openorcreate自己会创建的
            //文件不存在时创建
            //if (!finfo.Exists)
            //{
            //    FileStream fs = finfo.Create();
            //    fs.Close();
            //}
            */

            /*
             * 这个地方进行文件删除同样会有并发问题，改为下面以filemode的方式进行文件截断
            //判断文件是否存在以及是否大于2K  
            //if (finfo.Exists && finfo.Length > 204800)
            //{
            //    //删除该文件 
            //    finfo.Delete();
            //}
            */

            var fileMode = FileMode.OpenOrCreate;
            //20000KB截断为0
            if (finfo.Exists && finfo.Length > 20480000)
            {
                fileMode = FileMode.Truncate;
            }

            //以共享写的方式创建文件写入流，防止并发报错，它会自动进行线程排队，上一个线程写完了下一个线程才会进行写入
            //但是效率可能有点低，暂时业务上估计没那么大并发， 以后再说
            using (FileStream fs = new FileStream(finfo.FullName, fileMode, FileAccess.Write, FileShare.Write))
            //创建只写文件流  
            //using (FileStream fs = finfo.OpenWrite())//这种写法在并发的情况下会报文件被占用的错误，改用上面的写法即可
            {
                //根据上面创建的文件流创建写数据流  
                StreamWriter w = new StreamWriter(fs);
                try
                {
                    //设置写数据流的起始位置为文件流的末尾  
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    //写入当前系统时间并换行  
                    w.Write("{0:yyyy-MM-dd HH:mm:ss:fff}\r\n", DateTime.Now);
                    //写入日志内容并换行  
                    w.Write(input + "\r\n");
                    //写入----------------并换行  
                    w.Write("---------------------\r\n");
                    //清空缓冲区内容，并把缓冲区内容写入基础流  
                    w.Flush();
                }
                finally
                {
                    //关闭写数据流  
                    w.Close();
                    fs.Close();
                }
            }
        }
        public static void WriteHeartBeatLog(string message)
        {
            //Thread.Sleep(10000);
            string input = "【日志】\r\n" + message;

            //定义文件信息对象  
            FileInfo finfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "temp\\" + "heartBeat.txt");

            /*
             * 这里没必要判断，下边使用了openorcreate自己会创建的
            //文件不存在时创建
            //if (!finfo.Exists)
            //{
            //    FileStream fs = finfo.Create();
            //    fs.Close();
            //}
            */

            /*
             * 这个地方进行文件删除同样会有并发问题，改为下面以filemode的方式进行文件截断
            //判断文件是否存在以及是否大于2K  
            //if (finfo.Exists && finfo.Length > 204800)
            //{
            //    //删除该文件 
            //    finfo.Delete();
            //}
            */

            var fileMode = FileMode.OpenOrCreate;
            //20000KB截断为0
            if (finfo.Exists && finfo.Length > 20480000)
            {
                fileMode = FileMode.Truncate;
            }

            //以共享写的方式创建文件写入流，防止并发报错，它会自动进行线程排队，上一个线程写完了下一个线程才会进行写入
            //但是效率可能有点低，暂时业务上估计没那么大并发， 以后再说
            using (FileStream fs = new FileStream(finfo.FullName, fileMode, FileAccess.Write, FileShare.Write))
            //创建只写文件流  
            //using (FileStream fs = finfo.OpenWrite())//这种写法在并发的情况下会报文件被占用的错误，改用上面的写法即可
            {
                //根据上面创建的文件流创建写数据流  
                StreamWriter w = new StreamWriter(fs);
                try
                {
                    //设置写数据流的起始位置为文件流的末尾  
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    //写入当前系统时间并换行  
                    w.Write("{0:yyyy-MM-dd HH:mm:ss:fff}\r\n", DateTime.Now);
                    //写入日志内容并换行  
                    w.Write(input + "\r\n");
                    //写入----------------并换行  
                    w.Write("---------------------\r\n");
                    //清空缓冲区内容，并把缓冲区内容写入基础流  
                    w.Flush();
                }
                finally
                {
                    //关闭写数据流  
                    w.Close();
                    fs.Close();
                }
            }
        }

        #endregion
    }
}

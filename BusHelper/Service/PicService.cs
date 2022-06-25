 
 namespace BusHelper.Service;

 public  class PicService
    {
        // 流转文件
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[] 
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件 

            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// 文件转流
        public static Stream FileToStream(string fileName)

        {
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            // 把 byte[] 转换成 Stream 
            Stream stream = new MemoryStream(bytes);
            return stream;

        }

        /// 流转Bytes
        public static byte[] StreamToBytes(Stream stream)

        {

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 

            stream.Seek(0, SeekOrigin.Begin);
            return bytes;

        }

        //Bytes转流
        public static Stream BytesToStream(byte[] bytes)
        {

            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
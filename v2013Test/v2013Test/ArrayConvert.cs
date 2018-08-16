using System;

namespace ArrayConvert
{
    public class ByteArray
    {
        /// <summary>
        /// 将byte[]转换为byte
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的byte</returns>
        public static byte ToByte(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 1;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //返回转换结果
                return Convert.ToByte(tempArray);
            }
            catch
            {
                throw new Exception("异常:ArrayConvert.ByteArray.ToByte");
            }
        }

        /// <summary>
        /// 将byte[]转换为sbyte
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的sbyte</returns>
        public static sbyte ToSByte(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 1;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //返回转换结果
                return Convert.ToSByte(tempArray);
            }
            catch
            {
                throw new Exception("异常:ArrayConvert.ByteArray.ToSByte");
            }
        }


        /// <summary>
        /// 将byte[]转换为short
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的short</returns>
        public static short ToShort(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 2;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToInt16(tempArray, 0);
            }
            catch
            {
                throw new Exception("异常:ArrayConvert.ByteArray.ToShort");
            }
        }

        /// <summary>
        /// 将byte[]转换为ushort
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的ushort</returns>
        public static ushort ToUShort(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 2;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToUInt16(tempArray, 0);
            }
            catch
            {
                throw new Exception("异常:ArrayConvert.ByteArray.ToUShort");
            }
        }

        /// <summary>
        /// 将byte[]转换为int
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的int</returns>
        public static int ToInt(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 4;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToInt32(tempArray, 0);
            }
            catch
            {
                throw new Exception("异常:ArrayConvert.ByteArray.ToInt");
            }
        }

        /// <summary>
        /// 将byte[]转换为uint
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的uint</returns>
        public static uint ToUInt(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 4;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToUInt32(tempArray, 0);
            }
            catch
            {
                throw new Exception("异常:ArrayConvert.ByteArray.ToUInt");
            }
        }

        /// <summary>
        /// 将byte[]转换为long
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的long</returns>
        public static long ToLong(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 8;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToInt64(tempArray, 0);
            }
            catch
            {
                throw new Exception("异常:ArrayConvert.ByteArray.ToLong");
            }
        }

        /// <summary>
        /// 将byte[]转换为ulong
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的ulong</returns>
        public static ulong ToULong(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 8;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToUInt64(tempArray, 0);
            }
            catch
            {
                throw new Exception("异常:ArrayConvert.ByteArray.ToULong");
            }
        }

        /// <summary>
        /// 将byte[]转换为时间字符串
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <param name="length">数据的长度</param>
        /// <returns>转换后的时间(string)</returns>
        public static string ToSingleByteTime(byte[] sourceArray, int sourceStartIndex, int length)
        {
            string time = "";
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //计算转换结果
                foreach (byte i in tempArray)
                {
                    time = time + Convert.ToString(i).PadLeft(2, '0');
                }

                //返回转换结果
                return time;
            }
            catch
            {
                throw new Exception("异常:ArrayConvert.ByteArray.ToULong");
            }
        }

        //
    }
}
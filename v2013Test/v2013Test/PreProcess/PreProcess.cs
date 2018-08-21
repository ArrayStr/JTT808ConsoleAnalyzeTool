using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace v2013Test
{
    class PreProcess
    {
        /// <summary>
        /// 检查消息合法并转换成大写
        /// </summary>
        /// <param name="msg">待检查的消息</param>
        /// <returns>转换为大写的合法消息</returns>
        public static string VerifyEntireMessage(string msg)
        {
            const string hexPattern = "^[0-9A-Fa-f]+$";

            try
            {
                if (msg.Length < 15 * 2)    //最短的msg为终端心跳(0x0002),其长度为15个字节(即30个字符)
                {
                    double a = msg.Length / 2;
                    throw new Exception($"消息长度太短,这条消息仅有{Math.Ceiling(a)}字节,但是808消息长度不能低于15字节.");
                }
                else if (msg.Length % 2 != 0) //消息的字符数量必须为偶数
                {
                    throw new Exception("消息的字符数量必须是偶数.");
                }
                else if (!Regex.IsMatch(msg, hexPattern))   //消息中只能含有Hex字符
                {
                    throw new Exception("消息中含有非Hex字符.");
                }

                //返回转换结果
                return msg.ToUpper();
            }
            catch (Exception e)
            {
                throw new Exception($"函数VerifyMessage.EntireMessage异常:{e.Message}");
            }
        }


        /*
        public void SpliteMessage(byte[] msg, out byte[] msgStartTag, out byte[] msgHead, out byte[] msgBody, out byte[] msgCheckCode, out byte[] msgEndTag)
        {

        }
        */

        /// <summary>
        /// 计算消息的校验码是否正确
        /// </summary>
        /// <param name="msgHead">消息头</param>
        /// <param name="msgBody">消息体</param>
        /// <param name="msgCheckCode">消息校验码</param>
        public static void VerifyCheckCode(byte[] msgHead, byte[] msgBody, byte[] msgCheckCode)
        {
            List<byte> list = new List<byte>();

            try
            {
                //添加待校验的数据
                list.AddRange(msgHead);
                list.AddRange(msgBody);

                //校验
                if (msgCheckCode[0] != list.ToArray().Aggregate(0, (a, b) => b ^ a))
                {
                    throw new Exception("校验码错误.");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"函数VerifyMessage.CheckCode异常:{e.Message}");
            }
        }
    }
}

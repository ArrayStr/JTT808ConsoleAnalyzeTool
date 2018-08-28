using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using ArrayConverter;

namespace v2013Test
{
    class PreProcess
    {
        #region 属性
        //消息的各个部分
        public byte[] MsgStartTag { get; private set; }     //起始标识位
        public byte[] MsgHead { get; private set; }         //消息头
        public byte[] MsgBody { get; private set; }         //消息体
        public byte[] MsgCheckCodeag { get; private set; }  //检验码
        public byte[] MsgEndTag { get; private set; }       //结束标识位
        public ushort MsgId { get; private set; }           //消息ID
        #endregion

        #region 字段
        //消息全文
        private string msg; //字符串格式
        private byte[] msgHex;  //十六进制格式
        #endregion

        /// <summary>
        /// PreProcess
        /// </summary>
        /// <param name="input"></param>
        public PreProcess(string input)
        {

            try
            {
                msg = input;
                BytesConverter iBytesConverter = new BytesConverter();
                StringConverter iStringConverter = new StringConverter();

                //检验整条消息的合法性:  
                VerifyEntireMessage();

                //将消息转换为字节数组
                msgHex = iStringConverter.ToByteArray(msg, 16);

                //还原转义
                RestoreMessage();

                //解构消息
                SplitMessage();

                //提取消息ID
                MsgId = iBytesConverter.ToUShort(MsgHead, 0);

                //打印结果
                Console.WriteLine("\n---消息解构------------------");
                Console.WriteLine("{0}:{1}", nameof(MsgStartTag), BitConverter.ToString(MsgStartTag).Replace("-", string.Empty));
                Console.WriteLine("{0}:{1}", nameof(MsgHead), BitConverter.ToString(MsgHead).Replace("-", string.Empty));
                Console.WriteLine("{0}:{1}", nameof(MsgBody), BitConverter.ToString(MsgBody).Replace("-", string.Empty));
                Console.WriteLine("{0}:{1}", nameof(MsgCheckCodeag), BitConverter.ToString(MsgCheckCodeag).Replace("-", string.Empty));
                Console.WriteLine("{0}:{1}", nameof(MsgEndTag), BitConverter.ToString(MsgEndTag).Replace("-", string.Empty));
            }
            catch (Exception e)
            {
                throw new Exception($"异常类:{this.GetType().FullName}\n消息内容:{input}\n失败原因:{e.Message}"); 
            }
        }

        /// <summary>
        /// 检查消息合法并转换成大写
        /// </summary>
        public void VerifyEntireMessage()
        {
            const string _hexPattern = "^[0-9A-Fa-f]+$";
            
            try
            {
                if (msg.Length < 15 * 2)    //最短的msg为终端心跳(0x0002),其长度为15个字节(即30个字符)
                {
                    double a = msg.Length / 2;
                    throw new Exception($"消息长度太短.808消息长度应≥15字节,但是这条消息仅有{Math.Ceiling(a)}字节.");
                }
                else if (msg.Length % 2 != 0) //消息的字符数量必须为偶数
                {
                    throw new Exception("消息的字符数量必须是偶数.");
                }
                else if (!Regex.IsMatch(msg, _hexPattern))   //消息中只能含有Hex字符
                {
                    throw new Exception("消息中含有非Hex字符.");
                }
                else
                {
                    msg.ToUpper();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"VerifyEntireMessage函数异常->{e.Message}");
            }
        }

        /// <summary>
        /// 还原808消息
        /// </summary>
        private void RestoreMessage()
        {
            try
            {
                //将待处理ByteArray转换为List<byte>
                List<byte> _list = new List<byte>(msgHex);

                //0x7D 0x02 -> 0x7E
                for (int i = _list.Count - 3; i >= 1; i--)
                {
                    if (_list[i] == 0x7D && _list[i + 1] == 0x02)
                    {
                        _list[i] = 0x7E;
                        _list.RemoveAt(i + 1);
                    }
                }

                //0x7D 0x01 -> 0x7D
                for (int i = _list.Count - 3; i >= 1; i--)
                {
                    if (_list[i] == 0x7D && _list[i + 1] == 0x01)
                    {
                        _list.RemoveAt(i + 1);
                    }
                }

                //输出结果
                msgHex = _list.ToArray();
            }
            catch (Exception e)
            {
                throw new Exception($"RestoreMessage函数异常->{e.Message}");
            }

        }


        /// <summary>
        /// 解构808消息
        /// </summary>
        private void SplitMessage()
        {
            //定义消息各部分的长度
            uint msgStartTagLength = 1, msgHeadLength = 12, msgBodyLength, msgCheckCodeLength = 1, msgEndTagLength = 1;

            try
            {
                BytesConverter iBytesConverter = new BytesConverter();

                //获取起始标识位msgStartTag
                MsgStartTag = new byte[msgStartTagLength];
                Array.Copy(msgHex, 0, MsgStartTag, 0, msgStartTagLength);

                //获取消息头msgHead
                MsgHead = new byte[msgHeadLength];
                Array.Copy(msgHex, msgStartTagLength, MsgHead, 0, msgHeadLength);
               
                //获取消息体的长度msgBodyLength，单位bytes
                msgBodyLength = iBytesConverter.ToUShort(MsgHead, 2);
                msgBodyLength = msgBodyLength & 0x03FF;

                //获取消息体msgBody
                MsgBody = new byte[msgBodyLength];
                Array.Copy(msgHex, msgStartTagLength + msgHeadLength, MsgBody, 0, msgBodyLength);

                //获取校验码msgCheckCode
                MsgCheckCodeag = new byte[msgCheckCodeLength];
                Array.Copy(msgHex, msgStartTagLength + msgHeadLength + msgBodyLength, MsgCheckCodeag, 0, msgCheckCodeLength);

                //获取结束标识位msgEndTag
                MsgEndTag = new byte[msgEndTagLength];
                Array.Copy(msgHex, msgStartTagLength + msgHeadLength + msgBodyLength + msgCheckCodeLength, MsgEndTag, 0, msgEndTagLength);
            }
            catch (Exception e)
            {
                throw new Exception($"SplitMessage函数异常-> {e.Message}");
            }
        }

        /// <summary>
        /// 计算消息的检验码码是否正确
        /// </summary>
        private void VerifyCheckCode()
        {
            List<byte> list = new List<byte>();
 
            try
            {
                //添加待校验的数据
                list.AddRange(MsgHead);
                list.AddRange(MsgBody);

                //校验
                if (MsgCheckCodeag[0] != list.ToArray().Aggregate(0, (_a, _b) => _b ^ _a))
                {
                    throw new Exception($"检验码错误.");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"VerifyCheckCode函数异常->{e.Message}");
            }
        }
    }
}

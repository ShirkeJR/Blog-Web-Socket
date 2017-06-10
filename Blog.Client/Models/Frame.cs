using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Client
{
    public class Frame
    {
        public int FrameLength { get; set; }
        public int ParametresCount { get; set; }
        public string Command { get; set; }
        public string[] Parametres { get; set; }

        public Frame(string cmd, string[] array)
        {
            int temp = 0;
            Command = cmd;
            Parametres = array;
            if (array == null)
            {
                ParametresCount = 0;
                FrameLength = cmd.Length + 1;
            }  
            else
            {
                ParametresCount = array.Length;
                foreach (var param in Parametres)
                {
                    temp += param.Length + 1;
                }
                FrameLength = cmd.Length + 1 + temp;
            }
        }
        override public string ToString()
        {
            if (ParametresCount != 0)
                return String.Format("{0}\t{1}\t{3}\t/rn/rn$$", FrameLength, Command, String.Join("\t", Parametres));
            else
                return String.Format("{0}\t{1}\t/rn/rn$$", FrameLength, Command);
        }
    }
}

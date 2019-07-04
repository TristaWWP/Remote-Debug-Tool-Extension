using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSeverLib
{
    class ConstantContext
    {
        /// <summary>
        /// ReceiveDebugFile表示收到debug文件
        /// </summary>
        public const string ReceiveDebugFile = "RD";
        /// <summary>
        /// ReceiveRemoteFile表示收到remote文件
        /// </summary>
        public const string ReceiveRemoteFile = "RR";
        /// <summary>
        /// NoRemoteFile表示已有remote文件，不需要再发送remote文件
        /// </summary>
        public const string NoRemoteFile = "NR";
        /// <summary>
        /// zipDebugFile表示压缩后的debug文件的路径
        /// </summary>
        public const string zipDebugFile = "./debug";
        /// <summary>
        /// zipRemoteFile表示压缩后的remote文件的路径
        /// </summary>
        public const string zipRemoteFile = "./remote";

    }
}

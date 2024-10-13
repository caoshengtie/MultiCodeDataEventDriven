using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    internal class Window
    {
        #region field
        private string windowDesc;           //窗口描述
        private IntPtr windowIntPtr;           //窗口句柄
        private string windowTitle;           //窗口标题
        private string windowClass;           //窗口类
        private int windowLevel;           //窗口层级
        private int windowOrder;           //窗口顺序
        #endregion
        #region properity
        //窗口主键(唯一识别窗口)
        public string WindowDesc
        {
            get { return this.windowDesc; }
            set { this.windowDesc = value; }
        }
        //窗口句柄
        public IntPtr WindowIntPtr
        {
            get { return this.windowIntPtr; }
            set { this.windowIntPtr = value; }
        }
        //窗口标题
        public string WindowTitle
        {
            get { return this.windowTitle; }
            set { this.windowTitle = value; }
        }
        //窗口类
        public string WindowClass
        {
            get { return this.windowClass; }
            set { this.windowClass = value; }
        }
        //窗口层级
        public int WindowLevel
        {
            get { return this.windowLevel; }
            set { this.windowLevel = value; }
        }
        //窗口顺序
        public int WindowOrder
        {
            get { return this.windowOrder; }
            set { this.windowOrder = value; }
        }
        #endregion

    }
}

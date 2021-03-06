using System;
using System.Collections.Generic;
using System.Text;

namespace ZonTools.Shared
{
    public class WebSite
    {
        public Int32 Identity
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String PhysicalPath
        {
            get;
            set;
        }

        public ServerState Status
        {
            get;
            set;
        }
    }
}

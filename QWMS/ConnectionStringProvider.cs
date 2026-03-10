using ECMS.CommonLibrary.DBA;
using Newtonsoft.Json;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QWMS
{
    public class ConnectionStringProvider
    {

        public static ConnectionStringProvider Instance = new ConnectionStringProvider();

        Dictionary<string, string> _connections = new Dictionary<string, string>();

        public Dictionary<string, string> Connections
        {
            get { return _connections; }
            set { _connections = value; }
        }

        private ConnectionStringProvider()
        {
        }


        public void LoadConnections(string fileName)
        {
            string strFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);

            string strJsonText = getJsonText(strFilePath);
            List<ConnectionInfo> conns = JsonConvert.DeserializeObject<List<ConnectionInfo>>(strJsonText);

            foreach (ConnectionInfo conn in conns)
            {
                string strConnectionString = DBConnectionStringProvider.GetConnectionString(conn.Site, conn.SystemID, conn.LinkID, conn.RequestKey, conn.EncryptKey);
                string strConnectionTime = ";pooling=true;connection lifetime=0;min pool size = 1;max pool size=32767";
                _connections.Add(conn.DbCode, strConnectionString + strConnectionTime);
            }
        }

        string getJsonText(string path)
        {
            string strResult = string.Empty;
            string strLine = string.Empty;
            StreamReader reader = new StreamReader(path);
            while ((strLine = reader.ReadLine()) != null)
            {
                strResult += strLine;
            }
            return strResult;
        }
    }

    public class ConnectionInfo
    {
        /// <summary>
        /// Site
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// DbCode
        /// </summary>
        public string DbCode { get; set; }

        /// <summary>
        /// SystemID
        /// </summary>
        public string SystemID { get; set; }

        /// <summary>
        /// LinkID
        /// </summary>
        public string LinkID { get; set; }

        /// <summary>
        /// RequestKey
        /// </summary>
        public string RequestKey { get; set; }

        /// <summary>
        /// EncryptKey
        /// </summary>
        public string EncryptKey { get; set; }


    }
}

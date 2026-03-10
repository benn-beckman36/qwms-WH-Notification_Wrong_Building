using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Web.Mail;
using System.Xml;
using QWMS.Common;
using System.Configuration;

namespace QWMS
{
	public class InterfaceException : Exception
	{
		public InterfaceException(string message) : base(message)
		{
			
		}
		public InterfaceException(string message, Exception innerException) : base(message,innerException)
		{
			
		}
	}

	public class AccessConfig
	{
       
        
        
        private string strConfigFile = "";
		private string strCompany = "";
		private string strMandt = "";
        private string strComcd = "";
		private string strDBServer = "";
		private string strUserID = "";
		private string strPassword = "";
		private string strInitialCatalog = "";
		private string strConnectionString = "";
		private string strDefaultClient = "";
		private ArrayList arySQL = new ArrayList();




        







		public string ConfigFile
		{
			get
			{
				return strConfigFile;
			}
			set
			{
				strConfigFile = value;
			}
		}

		public string Company
		{
			get
			{
				return strCompany;
			}
			set
			{
				strCompany = value;
			}
		}

		public string Mandt
		{
			get
			{
				return strMandt;
			}
			set
			{
				strMandt = value;
			}
		}
        public string Comcd
        {
            get
            {
                return strComcd;
            }
            set
            {
                strComcd = value;
            }
        }
		public string DBServer
			  {
				  get
				  {
					  return strDBServer;
				  }
				  set
				  {
					  strDBServer = value;
				  }
			  }

		public string UserID
		{
			get
			{
				return strUserID;
			}
			set
			{
				strUserID = value;
			}
		}

		public string Password
		{
			get
			{
				return strPassword;
			}
			set
			{
				strPassword = value;
			}
		}
		public string InitialCatalog
		{
			get
			{
				return strInitialCatalog;
			}
			set
			{
				strInitialCatalog = value;
			}
		}
		public string ConnectionString
		{
			get
			{
				return strConnectionString;
			}
			set
			{
				strConnectionString = value;
			}
		}

		public string DefaultClient
		{
			get
			{
				return strDefaultClient;
			}
			set
			{
				strDefaultClient = value;
			}
		}


		public AccessConfig()
		{
            
            
           
		}

        public AccessConfig(string strComcd, string strConfigFile)
        {
            Company = strCompany;
            string strAppPath = "";
            strAppPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\config\\";
            //			aryAppPath = AppDomain.CurrentDomain.BaseDirectory.ToString().Split(new char[]{'\\'});
            //			for(int i=0;i<aryAppPath.Length-3;i++)
            //			{
            //				strAppPath += aryAppPath[i].ToString() + "\\";
            //			}	

            ConfigFile = strAppPath + strConfigFile;
            ParseConfigFile();
            GetConnectionString();

            //GetDBConfig();
        }
        public AccessConfig(string strComcd)
        {

            string strDBCode = strComcd.Trim();
            
            CommonInfo.Instance.DBType = int.Parse(ConfigurationSettings.AppSettings["DBType"].ToString().Trim());
            CommonInfo.Instance.DBCode = ConfigurationSettings.AppSettings["DBCode"].ToString().Trim() + strDBCode;
            CommonInfo.Instance.ErrType = int.Parse(ConfigurationSettings.AppSettings["ErrType"].ToString().Trim());
            CommonInfo.Instance.ErrCode = ConfigurationSettings.AppSettings["ErrCode"].ToString().Trim();
        }


       
		public void ParseConfigFile()
		{
			XmlTextReader xtrQWMS = null;
			string strTempName = "";
			bool bolFindClientConfig = false;
			try
			{
				xtrQWMS = new XmlTextReader(ConfigFile);
				while(xtrQWMS.Read())
				{
					switch(xtrQWMS.NodeType)
					{
						
                        
                        case XmlNodeType.Element:
							strTempName = xtrQWMS.Name;							
							break;
						case XmlNodeType.Text:

                            


							if(strTempName == "Default")
							{
								Company = xtrQWMS.Value;
							}
							
							if(strTempName == "Company")
							{
								if(xtrQWMS.Value == Company)
									bolFindClientConfig = true;
								else
									bolFindClientConfig = false;
							}
							if(bolFindClientConfig == true)
							{
								if(strTempName == "Mandt")
								{
									Mandt = xtrQWMS.Value;
								}
                                if (strTempName == "CompanyCode")
                                {
                                    Comcd = xtrQWMS.Value;
                                }
								if(strTempName == "Ddd")
								{
									char[] aryTemp = xtrQWMS.Value.ToCharArray();
									for(int i=0;i<aryTemp.Length;i++)
									{
										if(i%2 == 1 && i>0)
											DBServer += aryTemp[i].ToString();
									}
								}
								if(strTempName == "Aaa")
								{
									char[] aryTemp = xtrQWMS.Value.ToCharArray();
									for(int i=0;i<aryTemp.Length;i++)
									{
										if(i%2 == 1 && i>0)
											UserID += aryTemp[i].ToString();
									}
								}
								if(strTempName == "Bbb")
								{
									char[] aryTemp = xtrQWMS.Value.ToCharArray();
									for(int i=0;i<aryTemp.Length;i++)
									{
										if(i%2 == 1 && i>0)
											Password += aryTemp[i].ToString();
									}
								}
								if(strTempName == "Ccc")
								{
									char[] aryTemp = xtrQWMS.Value.ToCharArray();
									for(int i=0;i<aryTemp.Length;i++)
									{
										if(i%2 == 1 && i>0)
											InitialCatalog += aryTemp[i].ToString();
									}	
								}
							}
							break;
					}
				}
			}
			catch(Exception ex)
			{
				throw new InterfaceException(" Time:" + DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + "\r\n File:" + ConfigFile + "\r\n" + ex.Message.ToString());
			}
			finally
			{
				if(xtrQWMS != null)
					xtrQWMS.Close();
			}
		}

		private void GetConnectionString()
		{
			ConnectionString = "User id="+this.UserID+";password="+this.Password+";initial catalog="+this.InitialCatalog+";server="+this.DBServer;
		}
        private void GetDBConfig()
        {
           
            CommonInfo.Instance.DBType = int.Parse(ConfigurationSettings.AppSettings["DBType"].ToString().Trim());
            CommonInfo.Instance.DBCode = ConfigurationSettings.AppSettings["DBCode"].ToString().Trim();
            CommonInfo.Instance.ErrType = int.Parse(ConfigurationSettings.AppSettings["ErrType"].ToString().Trim());
            CommonInfo.Instance.ErrCode = ConfigurationSettings.AppSettings["ErrCode"].ToString().Trim();

        }
	}
}

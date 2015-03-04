using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace com.panik.discard {
	[DataContract]
	public class UserObj {
		public enum loginTypes {
			facebook,
			google
		};

//		private static readonly UserObj _instance = new UserObj ();

		[DataMember]
		public string id { get; set; }

		[DataMember]
		public string userKey { get; set; }

		[DataMember] // What service does the user use to login
		public int loginType { get; set; }

		public loginTypes loginTypeEnum { 
			get {
				return (loginTypes)loginType;
			}
			set {
				loginType = (int)value;
			}
		}

		[DataMember]
		public string name { get; set; }

		[DataMember]
		public string email { get; set; }

		[DataMember]
		public string deviceId { get; set; }

		[DataMember]
		public string deviceToClear { get; set; }

		[DataMember]
		public string updateDateTime { get; set; }
		// This field is for check syncing time to override fields
		public DateTime updateDateTimeObj {
			get {
				return DateTime.ParseExact (updateDateTime, "O", CultureInfo.InvariantCulture);
			}
			set {
				updateDateTime = value.ToString ("O", CultureInfo.InvariantCulture);
			}
		}

		[DataMember]
		public List<StoreObj> stores { get; set; }

		// TODO: Implement these
//		[DataMember]
//		public string country { get; set; }
//		// This field will override server on sync
//		[DataMember]
//		public string city { get; set; }
//		// This field will override server on sync
//		[DataMember]
//		public string recentLocation { get; set; }
//		// This field will override server on sync

		public UserObj () {
		}

		public string ToJson(){
			MemoryStream ms = new MemoryStream();
			new DataContractJsonSerializer (typeof(UserObj)).WriteObject (ms, this);
			ms.Position = 0;
			return new StreamReader (ms).ReadToEnd ();
		}

		public static UserObj ParseUserFromJson(string userJson){
			DataContractJsonSerializer userJsonSerializer = new DataContractJsonSerializer (typeof(UserObj));
			MemoryStream ms = new MemoryStream (Encoding.UTF8.GetBytes (userJson));
			ms.Position = 0;
			return (UserObj)userJsonSerializer.ReadObject (ms);
		}

//		public static UserObj instance {
//			get {
//				return _instance;
//			}
//			set {
//				_instance = value;
//			}
//		}
	}
}


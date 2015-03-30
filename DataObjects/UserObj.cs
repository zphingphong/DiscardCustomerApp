using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;

namespace com.panik.discard {
//	[DataContract]
	public class UserObj {
		public enum loginTypes {
			facebook,
			google
		};

//		private static readonly UserObj _instance = new UserObj ();

//		[DataMember]
		public string id { get; set; }

//		[DataMember]
		public string userKey { get; set; }

//		[DataMember] // What service does the user use to login
		public loginTypes loginType { get; set; }

//		public loginTypes loginTypeEnum { 
//			get {
//				return (loginTypes)loginType;
//			}
//			set {
//				loginType = (int)value;
//			}
//		}

//		[DataMember]
		public string name { get; set; }

//		[DataMember]
		public string email { get; set; }

//		[DataMember]
		public string deviceId { get; set; }

//		[DataMember]
		public string deviceToClear { get; set; }

//		[DataMember]
		public string updateDateTime { get; set; }

		// This field is for check syncing time to override fields
		[JsonIgnore]
		public DateTime updateDateTimeObj {
			get {
				return DateTime.ParseExact (updateDateTime, "O", CultureInfo.InvariantCulture);
			}
			set {
				updateDateTime = value.ToString ("O", CultureInfo.InvariantCulture);
			}
		}

		[JsonIgnore]
		public string qrImageDirectoryPath { get; set; }

//		[DataMember]
		public List<StoreObj> stores { get; set; }

		public List<StoreObj> cards { get; set; }

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
			return JsonConvert.SerializeObject (this);
		}

		public static UserObj ParseFromJson(string userJson){
			return JsonConvert.DeserializeObject<UserObj>(userJson);
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


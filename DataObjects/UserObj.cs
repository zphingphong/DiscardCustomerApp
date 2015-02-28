using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace com.panik.discard {
	[DataContract]
	public class UserObj {
		private static readonly UserObj _instance = new UserObj();
		[DataMember]
		public string id { get; set; }
		[DataMember]
		public string userKey { get; set; }
		[DataMember] // What service does the user use to login [0 = Facebook, 1 = Google]
		public int loginType { get; set; }
		[DataMember]
		public string name { get; set; }
		[DataMember]
		public string email { get; set; }
		[DataMember]
		public string deviceId { get; set; }
		[DataMember]
		public string updateDateTime { get; set; } // This field is for check syncing time to override fields
		public DateTime updateDateTimeObj {
			get {
				return DateTime.ParseExact(updateDateTime, "O", CultureInfo.InvariantCulture);
			}
			set {
				updateDateTime = value.ToString ("O", CultureInfo.InvariantCulture);
			}
		}
		[DataMember]
		public string country { get; set; } // This field will override server on sync
		[DataMember]
		public string city { get; set; } // This field will override server on sync
		[DataMember]
		public string recentLocation { get; set; } // This field will override server on sync

		private UserObj () {
		}

		public static UserObj instance {
			get {
				return _instance;
			}
		}
	}
}


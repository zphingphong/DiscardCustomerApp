using System;
using System.Collections.Generic;
//using System.Runtime.Serialization;

namespace com.panik.discard {
//	[DataContract]
	public class StoreObj {

//		[DataMember]
		public string _id { get; set; }

//		[DataMember]
		public int storeId { get; set; }

//		[DataMember]
		public string name { get; set; }

//		[DataMember]
		public string website { get; set; }

//		[DataMember]
		public int category { get; set; }

//		[DataMember]
		public List<string> emails { get; set; }

//		[DataMember]
		public List<string> phones { get; set; }

//		[DataMember]
		public AddressObj address { get; set; }

		public string uiXsLogoImagePath { get; set; }

		// TODO: Implement these
		//		[DataMember]
		//		public LocationObj location { get; set; }

		public StoreObj () {
		}
	}
}


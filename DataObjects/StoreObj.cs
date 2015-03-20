using System;
using System.Collections.Generic;
using Newtonsoft.Json;
//using System.Runtime.Serialization;
using System.ComponentModel;

namespace com.panik.discard {
//	[DataContract]
	public class StoreObj /*: INotifyPropertyChanged*/ {

//		public event PropertyChangedEventHandler PropertyChanged;

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

		public string ToJson(){
			return JsonConvert.SerializeObject (this);
		}

		public static StoreObj ParseFromJson(string storeJson){
			return JsonConvert.DeserializeObject<StoreObj>(storeJson);
		}
	}
}


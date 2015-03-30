using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.panik.discard {
	public class CardObj {
		public string _id { get; set; }
		public CardTemplateObj cardTemplate { get; set; }
		public string expiryDate { get; set; }
		public int stamp { get; set; }

		public CardObj () {
		}

		public string ToJson () {
			return JsonConvert.SerializeObject (this);
		}

		public static CardObj ParseFromJson (string cardJson) {
			return JsonConvert.DeserializeObject<CardObj> (cardJson);
		}
	}
}


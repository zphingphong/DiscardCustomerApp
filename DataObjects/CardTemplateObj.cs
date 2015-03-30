using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.panik.discard {
	public class CardTemplateObj {
		
		public enum cardTypes {
			membership,
			stampCard
		};

		public string _id { get; set; }
		[JsonProperty("store")]
		public string storeId { get; set; }
		public cardTypes type { get; set; }
		public string perks { get; set; }
		public int stampsToRedeem { get; set; }

		public CardTemplateObj () {
		}

		public string ToJson () {
			return JsonConvert.SerializeObject (this);
		}

		public static CardTemplateObj ParseFromJson (string cardTemplateJson) {
			return JsonConvert.DeserializeObject<CardTemplateObj> (cardTemplateJson);
		}
	}
}


using Newtonsoft.Json;

namespace Project.MVCUI.ShoppingTools
{
    [Serializable]
    public class CartItem
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; } = null!;

        [JsonProperty]
        public string Description { get; set; } = null!;

        [JsonProperty]
        public decimal Price { get; set; }

        [JsonProperty]
        public short Amount { get; set; }

        [JsonProperty]
        public short MaxAmount { get; set; }//For stock information

        [JsonProperty]
        public string ImagePath { get; set; } = null!;

        [JsonProperty]
        public decimal SubTotal 
        { 
            get
            {
                return Price * Amount;
            }
        }

        //For starts with 1
        public CartItem()
        {
            ++Amount;
        }
    }
}

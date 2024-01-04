using Newtonsoft.Json;

namespace Project.MVCUI.ShoppingTools
{
    [Serializable]
    public class Cart
    {
        [JsonProperty]
        private readonly Dictionary<int, CartItem> _basket;

        public Cart()
        {
            _basket = new Dictionary<int, CartItem>();
        }

        [JsonProperty]
        public ICollection<CartItem> Basket 
        {
            get
            {
                return _basket.Values;
            } 
        }

        [JsonProperty]
        public decimal TotalPrice 
        {
            get
            {
                return _basket.Sum(x => x.Value.SubTotal);
            }
        }

        [JsonProperty]
        public int TotalItemCount => _basket.Sum(x => x.Value.Amount);

        public void AddToBasket(CartItem item)
        {
            if (_basket.ContainsKey(item.Id))
            {
                if (item.Amount > 1) _basket[item.Id].Amount += item.Amount;
                else _basket[item.Id].Amount += 1;
            }
            else _basket.Add(item.Id, item);
        }

        public void RemoveFromBasket(int id)
        {
            if (_basket[id].Amount > 1) _basket[id].Amount -= 1;
            else _basket.Remove(id);
        }

        public void RemoveItemWithAllAmountFromBasket(int id) => _basket.Remove(id);
    }
}

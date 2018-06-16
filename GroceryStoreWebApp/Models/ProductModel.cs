using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GroceryStoreWebApp.Models
{
    public class ProductModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("UPC")]
        public double Upc { get; set; }
        [BsonElement("com_number")]
        public int ComNo { get; set; }
        [BsonElement("order_reference")]
        public int OrdRef { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("supplier")]
        public string Supplier { get; set; }

        public List<ProductModel> ProductList { get; set; }

        public string SearchToken { get; set; }
    }
}
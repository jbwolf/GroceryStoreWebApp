using System.Linq;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using GroceryStoreWebApp.App_Start;
using GroceryStoreWebApp.Models;

namespace GroceryStoreWebApp.Controllers
{
    public class ProductController : Controller
    {
        private MongoDBContext dbContext;
        private IMongoCollection<ProductModel> productCollection;
        private ProductModel productModel;

        public ProductController()
        {
            dbContext = new MongoDBContext();
            productCollection = dbContext.db.GetCollection<ProductModel>("products");
            productModel = new ProductModel();
        }

        // Display main page with textbox to perform searches
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string searchToken)
        {
            if (!string.IsNullOrEmpty(searchToken))
            {
                try
                {
                    var builder = Builders<ProductModel>.Filter;
                    var filter = builder.And(builder.Text(searchToken));
                    productModel.ProductList = productCollection.Find(filter).ToList();
                    productModel.SearchToken = searchToken;
                    return View(productModel);
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(string id, string searchToken)
        {
            var productId = new ObjectId(id);
            productModel = productCollection.AsQueryable<ProductModel>().SingleOrDefault(x => x.Id == productId);
            productModel.SearchToken = searchToken;
            return View(productModel);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel product)
        {
            try
            {
                productModel = product;
                productCollection.InsertOne(product);
                return RedirectToAction(product.SearchToken, "Product/Search");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id, string searchToken)
        {
            var productId = new ObjectId(id);
            productModel = productCollection.AsQueryable<ProductModel>().SingleOrDefault(x => x.Id == productId);
            productModel.SearchToken = searchToken;
            return View(productModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, ProductModel product)
        {
            try
            {
                var filter = Builders<ProductModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<ProductModel>.Update
                    .Set("description", product.Description)
                    .Set("UPC", product.Upc)
                    .Set("com_number", product.ComNo)
                    .Set("order_reference", product.OrdRef)
                    .Set("supplier", product.Supplier);
                var result = productCollection.UpdateOne(filter, update);
                return RedirectToAction(product.SearchToken, "Product/Search");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(string id, string searchToken)
        {
            var productId = new ObjectId(id);
            productModel = productCollection.AsQueryable<ProductModel>().SingleOrDefault(x => x.Id == productId);
            productModel.SearchToken = searchToken;
            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, ProductModel product)
        {
            try
            {
                productCollection.DeleteOne(Builders<ProductModel>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction(product.SearchToken, "Product/Search");
            }
            catch
            {
                return View();
            }
        }
    }
}

using System.Data.SqlClient;

namespace JoshuaWood_ST10296167_CLDV_POE.Models
{
    public class ProductDisplayModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public bool ProductAvailability { get; set; }

        //Default constructor
        public ProductDisplayModel() { }

        //Paramatized constructor
        public ProductDisplayModel(int id, string name, decimal price, string category, bool availability)
        {
            ProductID = id;
            ProductName = name;
            ProductPrice = price;
            ProductCategory = category;
            ProductAvailability = availability;
        }

        //This method retreivess all product details from the database and returns them to be displayed in a view
        public static List<ProductDisplayModel> DisplayProducts()
        {
            List<ProductDisplayModel> products = new List<ProductDisplayModel>();

            string con_string = ProductModel.con_string;
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT ProductID, ProductName, ProductPrice, ProductCategory, ProductAvailability FROM ProductTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductDisplayModel product = new ProductDisplayModel();
                    product.ProductID = Convert.ToInt32(reader["ProductID"]);
                    product.ProductName = Convert.ToString(reader["ProductName"]);
                    product.ProductPrice = Convert.ToDecimal(reader["ProductPrice"]);
                    product.ProductCategory = Convert.ToString(reader["ProductCategory"]);
                    product.ProductAvailability = Convert.ToBoolean(reader["ProductAvailability"]);
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }

        //This method retrieves the product information of certain products based off an associated prodcut ID
        public static ProductDisplayModel GetProductDetailsByUserID(int productID)
        {
            ProductDisplayModel product = new ProductDisplayModel();

            string con_string = ProductModel.con_string;
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT ProductName, ProductPrice, ProductCategory, ProductAvailability FROM ProductTable WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ProductID", productID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product.ProductName = Convert.ToString(reader["ProductName"]);
                    product.ProductPrice = Convert.ToDecimal(reader["ProductPrice"]);
                    product.ProductCategory = Convert.ToString(reader["ProductCategory"]);
                    product.ProductAvailability = Convert.ToBoolean(reader["ProductAvailability"]);
                }
                reader.Close();
            }
            return product;
        }
    }
}



namespace LINQSamples
{
    public class SamplesViewModel : ViewModelBase
    {
        #region InnerJoinQuery
        /// <summary>
        /// Join a Sales Order collection with Products into anonymous class
        /// NOTE: This is an equijoin or an inner join
        /// </summary>
        public List<ProductOrder> InnerJoinQuery()
        {
            List<ProductOrder> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();
            // Load all Sales Order Data
            List<SalesOrder> sales = SalesOrderRepository.GetAll();

            // Write Query Syntax Here
            list = (from product in products
                    join sale in sales on product.ProductID equals sale.ProductID
                    select new ProductOrder
                    {
                        ProductID = product.ProductID,
                        Name = product.Name,
                        Color = product.Color,
                        StandardCost = product.StandardCost,
                        ListPrice = product.ListPrice,
                        Size = product.Size,
                        OrderQty = sale.OrderQty,
                        UnitPrice = sale.UnitPrice,
                        LineTotal = sale.LineTotal
                    }).OrderBy(p => p.Name).ToList();

            return list;
        }
        #endregion

        #region InnerJoinMethod
        /// <summary>
        /// Join a Sales Order collection with Products into anonymous class
        /// NOTE: This is an equijoin or an inner join
        /// </summary>
        public List<ProductOrder> InnerJoinMethod()
        {
            List<ProductOrder> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();
            // Load all Sales Order Data
            List<SalesOrder> sales = SalesOrderRepository.GetAll();

            // Write Method Syntax Here
            list = products.Join(sales, product => product.ProductID, sale => sale.ProductID, (product, sale) => new ProductOrder
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Color = product.Color,
                StandardCost = product.StandardCost,
                ListPrice = product.ListPrice,
                Size = product.Size,
                OrderQty = sale.OrderQty,
                UnitPrice = sale.UnitPrice,
                LineTotal = sale.LineTotal
            }).OrderBy(product => product.Name).ToList();
            return list;
        }
        #endregion

        #region InnerJoinTwoFieldsQuery
        /// <summary>
        /// Join a Sales Order collection with Products collection using two fields
        /// </summary>
        public List<ProductOrder> InnerJoinTwoFieldsQuery()
        {
            List<ProductOrder> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();
            // Load all Sales Order Data
            List<SalesOrder> sales = SalesOrderRepository.GetAll();

            // Write Query Syntax Here
            list = (from product in products join sale in sales on new {product.ProductID,Qty = (short)6} equals new {sale.ProductID,Qty = sale.OrderQty} select new ProductOrder
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Color = product.Color,
                StandardCost = product.StandardCost,
                ListPrice = product.ListPrice,
                Size = product.Size,
                OrderQty = sale.OrderQty,
                UnitPrice = sale.UnitPrice,
                LineTotal = sale.LineTotal
            }).OrderBy(product => product.Name).ToList();

            return list;
        }
        #endregion

        #region InnerJoinTwoFieldsMethod
        /// <summary>
        /// Join a Sales Order collection with Products collection using two fields
        /// </summary>
        public List<ProductOrder> InnerJoinTwoFieldsMethod()
        {
            List<ProductOrder> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();
            // Load all Sales Order Data
            List<SalesOrder> sales = SalesOrderRepository.GetAll();

            // Write Method Syntax Here
            list = products.Join(sales, product => new { product.ProductID, Qty = (short)6 }, sale => new { sale.ProductID, Qty = sale.OrderQty }, (product, sale) => new ProductOrder
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Color = product.Color,
                StandardCost = product.StandardCost,
                ListPrice = product.ListPrice,
                Size = product.Size,
                OrderQty = sale.OrderQty,
                UnitPrice = sale.UnitPrice,
                LineTotal = sale.LineTotal
            }).OrderBy(p => p.Name).ToList();
            return list;
        }
        #endregion

        #region JoinIntoQuery
        /// <summary>
        /// Use 'into' to create a new object with a Sales collection for each Product
        /// This is like a combination of an inner join and left outer join
        /// The 'into' keyword allows you to put the sales into a 'sales' variable 
        /// that can be used to retrieve all sales for a specific product
        /// </summary>
        public List<ProductSales> JoinIntoQuery()
        {
            List<ProductSales> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();
            // Load all Sales Order Data
            List<SalesOrder> sales = SalesOrderRepository.GetAll();

            // Write Query Syntax Here
            list = (from product in products
                    join sale in sales on product.ProductID equals sale.ProductID into newsales
                    select new ProductSales
                    {
                        Product = product,
                        Sales = newsales.OrderBy(sale => sale.SalesOrderID).ToList()
                    }).ToList();

            return list;
        }
        #endregion

        #region JoinIntoMethod
        /// <summary>
        /// Use GroupJoin() to create a new object with a Sales collection for each Product
        /// This is like a combination of an inner join and left outer join
        /// The GroupJoin() method replaces the into keyword
        /// </summary>
        public List<ProductSales> JoinIntoMethod()
        {
            List<ProductSales> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();
            // Load all Sales Order Data
            List<SalesOrder> sales = SalesOrderRepository.GetAll();

            // Write Method Syntax Here
            list = products.GroupJoin(sales, p => p.ProductID, s => s.ProductID, (p, newSales) => new ProductSales
            {
                Product = p,
                Sales = newSales.OrderBy(s => s.SalesOrderID).ToList()
            }).ToList();

            return list;
        }
        #endregion

        #region LeftOuterJoinQuery
        /// <summary>
        /// Perform a left join between Products and Sales using DefaultIfEmpty() and SelectMany()
        /// </summary>
        public List<ProductOrder> LeftOuterJoinQuery()
        {
            List<ProductOrder> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();
            // Load all Sales Order Data
            List<SalesOrder> sales = SalesOrderRepository.GetAll();

            // Write Query Syntax Here
            list = (from product in products join sale in sales on product.ProductID equals sale.ProductID into newSales from sale in newSales.DefaultIfEmpty() select new ProductOrder
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Color = product.Color,
                StandardCost = product.StandardCost,
                ListPrice = product.ListPrice,
                Size = product.Size,
                OrderQty = sale?.OrderQty,
                UnitPrice = sale?.UnitPrice,
                LineTotal = sale?.LineTotal
            }).ToList();

            return list;
        }
        #endregion

        #region LeftOuterJoinMethod
        /// <summary>
        /// Perform a left join between Products and Sales using DefaultIfEmpty() and SelectMany()
        /// </summary>
        public List<ProductOrder> LeftOuterJoinMethod()
        {
            List<ProductOrder> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();
            // Load all Sales Order Data
            List<SalesOrder> sales = SalesOrderRepository.GetAll();

            // Write Method Syntax Here
            list = products.SelectMany(prod => sales.Where(s => s.ProductID == prod.ProductID).DefaultIfEmpty(), (prod, sale) => new ProductOrder
            {
                ProductID = prod.ProductID,
                Name = prod.Name,
                Color = prod.Color,
                StandardCost = prod.StandardCost,
                ListPrice = prod.ListPrice,
                Size = prod.Size,
                OrderQty = sale?.OrderQty,
                UnitPrice = sale?.UnitPrice,
                LineTotal = sale?.LineTotal

            }).ToList();

            return list;
        }
        #endregion
    }
}

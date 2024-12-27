using System.Diagnostics.CodeAnalysis;

namespace LINQSamples
{
    
    public class SamplesViewModel : ViewModelBase
    {
        #region UnionIntegersQuery
        /// <summary>
        /// Union() combines two lists together, but skips duplicates
        /// This is like the UNION SQL operator
        /// </summary>
        public List<int> UnionIntegersQuery()
        {
            List<int> list = null;
            // Create a list of numbers
            List<int> list1 = new() { 5, 2, 3, 4, 5 };
            // Create a list of numbers
            List<int> list2 = new() { 1, 2, 3, 4, 5 };

            // Write Query Syntax Here
            list = (from num in list1 select num).Union((from num in list2 select num)).ToList();

            return list;
        }
        #endregion

        #region UnionIntegersMethod
        /// <summary>
        /// Union() combines two lists together, but skips duplicates
        /// This is like the UNION SQL operator
        /// </summary>
        public List<int> UnionIntegersMethod()
        {
            List<int> list = null;
            // Create a list of numbers
            List<int> list1 = new() { 5, 2, 3, 4, 5 };
            // Create a list of numbers
            List<int> list2 = new() { 1, 2, 3, 4, 5 };

            // Write Query Syntax Here
            list = list1.Union(list2).OrderBy(num=>num).ToList();

            return list;
        }
        #endregion

        #region UnionQuery
        /// <summary>
        /// Union() combines two lists together, but skips duplicates by using a comparer class
        /// This is like the UNION SQL operator
        /// </summary>
        public List<Product> UnionQuery()
        {
            List<Product> list = null;
            ProductComparer pc = new();
            // Load all Product Data
            List<Product> list1 = ProductRepository.GetAll();
            // Load all Product Data
            List<Product> list2 = ProductRepository.GetAll();

            // Write Query Syntax Here
            list = (from p in list1 select p).Union((from p in list2 select p),pc).ToList();

            return list;
        }
        #endregion

        #region UnionMethod
        /// <summary>
        /// Union() combines two lists together, but skips duplicates by using a comparer class
        /// This is like the UNION SQL operator
        /// </summary>
        public List<Product> UnionMethod()
        {
            List<Product> list = null;
            ProductComparer pc = new();
            // Load all Product Data
            List<Product> list1 = ProductRepository.GetAll();
            // Load all Product Data
            List<Product> list2 = ProductRepository.GetAll();

            // Write Method Syntax Here
            list = list1.Union(list2, pc).ToList();

            return list;
        }
        #endregion

        #region UnionByQuery
        /// <summary>
        /// UnionBy() combines two lists together using DISTINCT on the property specified. 
        /// </summary>
        public List<Product> UnionByQuery()
        {
            List<Product> list = null;
            // Load all Product Data
            List<Product> list1 = ProductRepository.GetAll();
            // Load all Product Data
            List<Product> list2 = ProductRepository.GetAll();

            // Write Query Syntax Here
            list = (from product in list1 select product).UnionBy(list2,p=>p.Color).ToList();

            return list;
        }
        #endregion

        #region UnionByMethod
        /// <summary>
        /// UnionBy() combines two lists together using DISTINCT on the property specified. 
        /// </summary>
        public List<Product> UnionByMethod()
        {
            List<Product> list = null;
            // Load all Product Data
            List<Product> list1 = ProductRepository.GetAll();
            // Load all Product Data
            List<Product> list2 = ProductRepository.GetAll();

            // Write Method Syntax Here
            list = list1.UnionBy(list2, p => p.Color).ToList();

            return list;
        }
        #endregion

        #region ConcatIntegersQuery
        /// <summary>
        /// The Concat() operator combines two lists together and does NOT check for duplicates
        /// This is like the UNION ALL SQL operator
        /// </summary>
        public List<int> ConcatIntegersQuery()
        {
            List<int> list = null;
            // Create a list of numbers
            List<int> list1 = new() { 5, 2, 3, 4, 5 };
            // Create a list of numbers
            List<int> list2 = new() { 1, 2, 3, 4, 5 };

            // Write Query Syntax Here
            list = (from num in list1 select num).Concat(list2).ToList();

            return list;
        }
        #endregion

        #region ConcatIntegersMethod
        /// <summary>
        /// The Concat() operator combines two lists together and does NOT check for duplicates
        /// This is like the UNION ALL SQL operator
        /// </summary>
        public List<int> ConcatIntegersMethod()
        {
            List<int> list = null;
            // Create a list of numbers
            List<int> list1 = new() { 5, 2, 3, 4, 5 };
            // Create a list of numbers
            List<int> list2 = new() { 1, 2, 3, 4, 5 };

            // Write Query Syntax Here
            list = list2.Concat(list1).ToList();

            return list;
        }
        #endregion

        #region ConcatQuery
        /// <summary>
        /// The Concat() operator combines two lists together and does NOT check for duplicates
        /// This is like the UNION ALL SQL operator
        /// </summary>
        public List<Product> ConcatQuery()
        {
            List<Product> list = null;
            // Load all Product Data
            List<Product> list1 = ProductRepository.GetAll();
            // Load all Product Data
            List<Product> list2 = ProductRepository.GetAll();

            // Write Query Syntax Here
            list = (from prod in list1 select prod).Concat(list2).ToList();

            return list;
        }
        #endregion

        #region ConcatMethod
        /// <summary>
        /// The Concat() operator combines two lists together and does NOT check for duplicates
        /// This is like the UNION ALL SQL operator
        /// </summary>
        public List<Product> ConcatMethod()
        {
            List<Product> list = null;
            // Load all Product Data
            List<Product> list1 = ProductRepository.GetAll();
            // Load all Product Data
            List<Product> list2 = ProductRepository.GetAll();

            // Write Method Syntax Here
            list = list1.Concat(list2).ToList();

            return list;
        }
        #endregion
    }
}

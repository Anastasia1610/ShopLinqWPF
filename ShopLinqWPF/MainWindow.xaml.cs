using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopLinqWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (var item in products)
            {
                ProductListBox.Items.Add(item);
            }
        }

        List<Product> products = new List<Product>() { new Product("Apple", "Fruit", 20), new Product("Banana", "Fruit", 40),
        new Product("Orange", "Fruit", 30), new Product("Strawberry", "Fruit", 400), new Product("Pinapple", "Fruit", 200),
        new Product("Avocado", "Fruit", 260), new Product("Carrot", "Vegetables", 40), new Product("Onion", "Vegetables", 15),
        new Product("Potato", "Vegetables", 20), new Product("Pepper", "Vegetables", 110), new Product("Cucumber", "Vegetables", 70),
        new Product("Tomato", "Vegetables", 90), new Product("Milk", "Dairy", 40), new Product("Butter", "Dairy", 100),
        new Product("Cheese", "Dairy", 170), new Product("Yoghurt", "Dairy", 60), new Product("Chicken", "Meat", 95),
        new Product("Turkey", "Meat", 180),  new Product("Pork", "Meat", 200),  new Product("Beef", "Meat", 240),
        new Product("Salmon", "Seafood", 600), new Product("Herring", "Seafood", 80), new Product("Salmon", "Seafood", 600),
        new Product("Shrimp", "Seafood", 350), new Product("Rice", "Grains", 60), new Product("Buckwheat", "Grains", 35),
        new Product("Beans", "Grains", 70), new Product("Cake", "Sweets", 300), new Product("Candies", "Sweets", 110),
        new Product("Coockies", "Sweets", 80), new Product("Marshmello", "Sweets", 65), new Product("Chocolate", "Sweets", 150)}; 
        
        class Product
        {
            public Product(string name, string category, int price)
            {
                Name = name;
                Category = category;
                Price = price;
            }

            public string Name { get; set; }
            public string Category { get; set; } 
            public int Price { get; set; }

            public override string ToString()
            {
                return $"{Name}, {Price}$. \t({Category})";
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            // Category Filter
            if(!string.IsNullOrWhiteSpace(CategoryFilter.Text) && string.IsNullOrWhiteSpace(PriceFrom.Text) 
                && string.IsNullOrWhiteSpace(PriceTo.Text))
            {
                // Создание листа продуктов выбранной категории
                var newProducts = from product in products
                                  where product.Category == CategoryFilter.Text
                                  select product;
                // Очищение ProductListBox
                ProductListBox.Items.Clear();
                // Заполение созданным списком продуктов выбранной категории
                foreach (var item in newProducts)
                {
                    ProductListBox.Items.Add(item);
                }
            }
        }
    }
}

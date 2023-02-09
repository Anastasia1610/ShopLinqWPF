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
            // Если заполнен только TextBox Category, а PriceFrom и PriceTo пустые
            if (!string.IsNullOrWhiteSpace(CategoryFilter.Text) && string.IsNullOrWhiteSpace(PriceFrom.Text)
                && string.IsNullOrWhiteSpace(PriceTo.Text))
            {
                // Создание листа продуктов выбранной категории
                var newProducts = from product in products
                                  where product.Category.ToLower() == CategoryFilter.Text.ToLower()
                                  select product;
                // Очищение ProductListBox
                ProductListBox.Items.Clear();

                // Вывод сообщения в ListBox, если товар не найден
                // Или заполение созданным списком продуктов выбранной категории
                if (newProducts.Count() == 0)
                    ProductListBox.Items.Add("Nothing found");
                else  
                    foreach (var item in newProducts)
                        ProductListBox.Items.Add(item);
            }

            // Price Filter
            // Если TextBox Category пустой, а заполнены только PriceFrom и PriceTo
            if (string.IsNullOrWhiteSpace(CategoryFilter.Text) && !string.IsNullOrWhiteSpace(PriceFrom.Text)
                && !string.IsNullOrWhiteSpace(PriceTo.Text))
            {
                // Если PriceFrom и PriceTo распарсились удачно
                if (int.TryParse(PriceFrom.Text, out int priceFrom) && int.TryParse(PriceTo.Text, out int priceTo))
                {
                    // Проверка на соответствие цен
                    if(priceFrom > priceTo || priceFrom < 0)
                    {
                        MessageBox.Show("Wrong price", "Error", MessageBoxButton.OK);
                        PriceFrom.Text = "";
                        PriceTo.Text = "";
                    }
                    else
                    {
                        // Создание листа продуктов с соответствующей ценой
                        var newProducts = from product in products
                                          where product.Price >= priceFrom && product.Price <= priceTo
                                          select product;
                        // Очищение ProductListBox
                        ProductListBox.Items.Clear();

                        // Вывод сообщения в ListBox, если товар не найден
                        // Или заполение созданным списком продуктов c соответствующей ценой
                        if (newProducts.Count() == 0)
                            ProductListBox.Items.Add("Nothing found");
                        else
                            foreach (var item in newProducts)
                                ProductListBox.Items.Add(item);
                    }
                }
                else  // Если PriceFrom и/или PriceTo не распарсились
                {
                    MessageBox.Show("Wrong price", "Error", MessageBoxButton.OK);
                    PriceFrom.Text = "";
                    PriceTo.Text = "";
                }
            }

            // Category Filter and Price Filter together
            if (!string.IsNullOrWhiteSpace(CategoryFilter.Text) && !string.IsNullOrWhiteSpace(PriceFrom.Text)
                && !string.IsNullOrWhiteSpace(PriceTo.Text))
            {
                // Если PriceFrom и PriceTo распарсились удачно
                if (int.TryParse(PriceFrom.Text, out int priceFrom) && int.TryParse(PriceTo.Text, out int priceTo))
                {
                    // Проверка на соответствие цен
                    if (priceFrom > priceTo || priceFrom < 0)
                    {
                        MessageBox.Show("Wrong price", "Error", MessageBoxButton.OK);
                        PriceFrom.Text = "";
                        PriceTo.Text = "";
                    }
                    else
                    {
                        // Создание листа продуктов с соответствующей категорией и ценой
                        var newProducts = from product in (from item in products
                                                           where item.Category.ToLower() == CategoryFilter.Text.ToLower()
                                                           select item)
                                          where product.Price >= priceFrom && product.Price <= priceTo
                                          select product;
                        // Очищение ProductListBox
                        ProductListBox.Items.Clear();

                        // Вывод сообщения в ListBox, если товар не найден
                        // Или заполение созданным списком продуктов c соответствующей ценой
                        if (newProducts.Count() == 0)
                            ProductListBox.Items.Add("Nothing found");
                        else
                            foreach (var item in newProducts)
                                ProductListBox.Items.Add(item);
                    }
                }
                else  // Если PriceFrom и/или PriceTo не распарсились
                {
                    MessageBox.Show("Wrong price", "Error", MessageBoxButton.OK);
                    PriceFrom.Text = "";
                    PriceTo.Text = "";
                }
            }

            // добавить еще другие комбинации заполнения фильтров
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListBox.Items.Clear(); 
            foreach (var item in products)
            {
                ProductListBox.Items.Add(item);
            }
        }

        private void NameSearchButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(NameSearchTextBox.Text))
            {
                var newProducts = from product in products
                                  where product.Name.ToLower() == NameSearchTextBox.Text.ToLower()
                                  select product;

                ProductListBox.Items.Clear();

                foreach (var item in newProducts)
                {
                    ProductListBox.Items.Add(item);
                }
            }
        }

        
    }
}

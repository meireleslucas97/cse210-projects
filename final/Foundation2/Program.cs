using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Criando produtos
        Product produto1 = new Product("Produto 1", 1, 3.5, 2);
        Product produto2 = new Product("Produto 2", 2, 5.0, 3);

        // Criando cliente e endereço
        Address enderecoCliente = new Address("123 Main St", "Cidade", "Estado", "País");
        Customer cliente = new Customer("Nome do Cliente", enderecoCliente);

        // Criando pedido e adicionando produtos
        Order pedido = new Order(cliente);
        pedido.AddProduct(produto1);
        pedido.AddProduct(produto2);

        // Obtendo e exibindo resultados
        Console.WriteLine("Etiqueta de Embalagem:\n" + pedido.GetPackageLabel());
        Console.WriteLine("\nEtiqueta de Envio:\n" + pedido.GetShippingLabel());
        Console.WriteLine("\nPreço Total do Pedido: $" + pedido.GetTotalPrice());
    }
}

class Order
{
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        this.products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetTotalPrice()
    {
        double total = 0;
        foreach (Product product in products)
        {
            total += product.GetTotalCost();
        }
        total += customer.Address.IsInUSA() ? 5 : 35; // Custo de envio
        return total;
    }

    public string GetPackageLabel()
    {
        string label = "Etiqueta de Embalagem:\n";
        foreach (Product product in products)
        {
            label += $"{product.Name} (ID: {product.ProductID}) - {product.GetTotalCost():C}\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return "Etiqueta de Envio:\n" + customer.GetShippingInfo();
    }
}

class Product
{
    private string name;
    private int productID;
    private double unitPrice;
    private int quantity;

    public Product(string name, int productID, double unitPrice, int quantity)
    {
        this.name = name;
        this.productID = productID;
        this.unitPrice = unitPrice;
        this.quantity = quantity;
    }

    public double GetTotalCost()
    {
        return unitPrice * quantity;
    }

    public string Name { get { return name; } }
    public int ProductID { get { return productID; } }
}

class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public Address Address { get { return address; } }

    public string GetShippingInfo()
    {
        return $"{name}\n{address.GetFullAddress()}";
    }
}

class Address
{
    private string streetAddress;
    private string city;
    private string state;
    private string country;

    public Address(string streetAddress, string city, string state, string country)
    {
        this.streetAddress = streetAddress;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.Equals("USA", StringComparison.OrdinalIgnoreCase);
    }

    public string GetFullAddress()
    {
        return $"{streetAddress}\n{city}, {state}\n{country}";
    }
}

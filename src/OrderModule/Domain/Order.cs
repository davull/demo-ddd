namespace OrderModule.Domain;

public class Order
{
    public Customer Customer { get; }
    public Address ShippingAddress { get; }
    public Address BillingAddress { get; }
    public OrderItemCollection Items { get; }

    public Order(
        Customer customer,
        Address shippingAddress, 
        Address billingAddress,
        OrderItemCollection items)
    {
        Customer = customer;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Items = items;
    }

    public override string ToString() => $"{nameof(Customer)}: {Customer}";
}
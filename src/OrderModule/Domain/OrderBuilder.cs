using SharedKernel;

namespace OrderModule.Domain;

public class OrderBuilder
{
    private Customer? _customer;
    private Address? _billingAddress;
    private Address? _shippingAddress;
    private readonly List<OrderItem> _items = new();

    public OrderBuilder ForCustomer(Customer customer)
    {
        _customer = customer;
        return this;
    }

    public OrderBuilder WithBillingAddress(Address billingAddress)
    {
        _billingAddress = billingAddress;
        return this;
    }

    public OrderBuilder WithShippingAddress(Address shippingAddress)
    {
        _shippingAddress = shippingAddress;
        return this;
    }

    public OrderBuilder WithItem(OrderItem item)
    {
        _items.Add(item);
        return this;
    }

    public OrderBuilder WithItems(IEnumerable<OrderItem> items)
    {
        items.Do(i => WithItem(i));

        return this;
    }

    public Order Build()
    {
        if (_customer is null)
            throw new InvalidOperationException("Customer is required");

        if (_billingAddress is null)
            throw new InvalidOperationException("Billing address is required");

        return new Order(
            customer: _customer,
            shippingAddress: _shippingAddress ?? _billingAddress,
            billingAddress: _billingAddress,
            items: new OrderItemCollection(_items));
    }
}
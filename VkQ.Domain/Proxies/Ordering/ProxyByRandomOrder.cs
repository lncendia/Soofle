using VkQ.Domain.Ordering.Abstractions;
using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Proxies.Ordering.Visitor;

namespace VkQ.Domain.Proxies.Ordering;

public class ProxyByRandomOrder : IOrderBy<Proxy, IProxySortingVisitor>
{
    public IEnumerable<Proxy> Order(IEnumerable<Proxy> items)
    {
        var rnd = new Random();
        return items.OrderBy(x => rnd.Next());
    }

    public IList<IEnumerable<Proxy>> Divide(IEnumerable<Proxy> items) => Order(items).Select(x => new List<Proxy> { x }.AsEnumerable()).ToList();

    public void Accept(IProxySortingVisitor visitor) => visitor.Visit(this);
}
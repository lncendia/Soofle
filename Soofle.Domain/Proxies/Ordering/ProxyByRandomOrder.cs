using Soofle.Domain.Ordering.Abstractions;
using Soofle.Domain.Proxies.Entities;
using Soofle.Domain.Proxies.Ordering.Visitor;

namespace Soofle.Domain.Proxies.Ordering;

public class ProxyByRandomOrder : IOrderBy<Proxy, IProxySortingVisitor>
{
    public IEnumerable<Proxy> Order(IEnumerable<Proxy> items)
    {
        var rnd = new Random();
        return items.OrderBy(_ => rnd.Next());
    }

    public IList<IEnumerable<Proxy>> Divide(IEnumerable<Proxy> items) => Order(items).Select(x => new List<Proxy> { x }.AsEnumerable()).ToList();

    public void Accept(IProxySortingVisitor visitor) => visitor.Visit(this);
}
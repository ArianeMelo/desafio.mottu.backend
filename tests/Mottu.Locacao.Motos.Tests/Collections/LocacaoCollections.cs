using Mottu.Locacao.Motos.Tests.Fixture;
using Xunit;

namespace Mottu.Locacao.Motos.Tests.Collections
{
 
    [CollectionDefinition(nameof(LocacaoCollections))]
    public class LocacaoCollections
        : ICollectionFixture<LocacaoServiceFixture>
    { }
}

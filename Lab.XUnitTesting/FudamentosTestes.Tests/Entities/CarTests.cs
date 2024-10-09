using Bogus;
using FluentAssertions;
using FudamentosTestes.Entities;
using Xunit.Abstractions;

namespace FudamentosTestes.Tests.Entities;

[Trait("Category", "Car")]
public sealed class CarTests
{
    private readonly Faker _faker = new("pt_BR");
    private readonly ITestOutputHelper _testOutputHelper;

    public CarTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("Carro 1")]
    [InlineData("Carro 2")]
    public void ValidaConstrutor_DadoTodosParametros_EntaoAtribuirAsPropriedadesCorretamente(string nomeCarroEsperado)
    {
        // Arrange
        // Pegar parametros
        var idCarroEsperado = Guid.NewGuid();
        
        // Act
        // Montar objeto
        var carro = new Car(idCarroEsperado, nomeCarroEsperado);

        // Assert
        // Validar
        Assert.Equal(idCarroEsperado, carro.Id);
        Assert.Equal(nomeCarroEsperado, carro.Name);
    }
    
    [Fact]
    public void ValidaConstrutor_DadoParametros_EntaoAtribuirAsPropriedadesCorretamente()
    {
        // Arrange
        // Pegar parametros
        var idCarroEsperado = Guid.NewGuid();
        var nomeCarroEsperado = "Carro";
        
        // Act
        var carro = new Car(idCarroEsperado, nomeCarroEsperado);

        // Assert
        Assert.Equal(idCarroEsperado, carro.Id);
        Assert.Equal(nomeCarroEsperado, carro.Name);
    }
    
    [Fact]
    public void Faker_ValidaConstrutor_DadoParametros_EntaoAtribuirAsPropriedadesCorretamente()
    {
        // Arrange
        var nomeCarroEsperado = _faker.Person.FirstName;
        var idCarroEsperado = Guid.NewGuid();

        _testOutputHelper.WriteLine(nomeCarroEsperado);
        
        // Act
        var carro = new Car(idCarroEsperado, nomeCarroEsperado);

        // Assert
        Assert.Equal(idCarroEsperado, carro.Id);
        Assert.Equal(nomeCarroEsperado, carro.Name);
    }
    
    [Fact]
    public void FluentAssertions_ValidaConstrutor_DadoParametros_EntaoAtribuirAsPropriedadesCorretamente()
    {
        // Arrange
        var nomeCarroEsperado = _faker.Person.FirstName;
        var idCarroEsperado = Guid.NewGuid();

        _testOutputHelper.WriteLine(nomeCarroEsperado);
        
        // Act
        var carro = new Car(idCarroEsperado, nomeCarroEsperado);

        carro.Id.Should().Be(idCarroEsperado, "Deve ser igual");
        carro.Name.Should().Be(nomeCarroEsperado);
    }

}
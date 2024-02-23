using System;

// Classe base para eventos
class Evento
{
    private string titulo;
    private string descricao;
    private DateTime data;
    private TimeSpan hora;
    private Endereco endereco;

    public Evento(string titulo, string descricao, DateTime data, TimeSpan hora, Endereco endereco)
    {
        this.titulo = titulo;
        this.descricao = descricao;
        this.data = data;
        this.hora = hora;
        this.endereco = endereco;
    }

    // Métodos para retornar mensagens
    public string ObterDetalhesPadrao()
    {
        return $"Detalhes Padrão:\nTítulo: {titulo}\nDescrição: {descricao}\nData: {data.ToShortDateString()}\nHora: {hora}\nEndereço: {endereco}";
    }

    public virtual string ObterDetalhesCompletos()
    {
        return ObterDetalhesPadrao();
    }

    public string ObterBreveDescricao()
    {
        return $"Breve Descrição:\nTipo: {this.GetType().Name}\nTítulo: {titulo}\nData: {data.ToShortDateString()}";
    }
}

// Classe para endereço
class Endereco
{
    private string rua;
    private string cidade;
    private string estado;
    private string cep;

    public Endereco(string rua, string cidade, string estado, string cep)
    {
        this.rua = rua;
        this.cidade = cidade;
        this.estado = estado;
        this.cep = cep;
    }

    public override string ToString()
    {
        return $"{rua}, {cidade}, {estado}, {cep}";
    }
}

// Classe derivada para palestras
class Palestra : Evento
{
    private string nomePalestrante;
    private int capacidadePalestrante;

    public Palestra(string titulo, string descricao, DateTime data, TimeSpan hora, Endereco endereco, string nomePalestrante, int capacidadePalestrante)
        : base(titulo, descricao, data, hora, endereco)
    {
        this.nomePalestrante = nomePalestrante;
        this.capacidadePalestrante = capacidadePalestrante;
    }

    public override string ObterDetalhesCompletos()
    {
        return $"{base.ObterDetalhesCompletos()}\nDetalhes Palestra:\nNome do Palestrante: {nomePalestrante}\nCapacidade do Palestrante: {capacidadePalestrante}";
    }
}

// Classe derivada para recepções
class Recepcao : Evento
{
    private string emailConfirmacao;

    public Recepcao(string titulo, string descricao, DateTime data, TimeSpan hora, Endereco endereco, string emailConfirmacao)
        : base(titulo, descricao, data, hora, endereco)
    {
        this.emailConfirmacao = emailConfirmacao;
    }

    public override string ObterDetalhesCompletos()
    {
        return $"{base.ObterDetalhesCompletos()}\nDetalhes Recepção:\nE-mail de Confirmação: {emailConfirmacao}";
    }
}

// Classe derivada para reuniões ao ar livre
class ReuniaoAoArLivre : Evento
{
    private string previsaoTempo;

    public ReuniaoAoArLivre(string titulo, string descricao, DateTime data, TimeSpan hora, Endereco endereco, string previsaoTempo)
        : base(titulo, descricao, data, hora, endereco)
    {
        this.previsaoTempo = previsaoTempo;
    }

    public override string ObterDetalhesCompletos()
    {
        return $"{base.ObterDetalhesCompletos()}\nDetalhes Reunião ao Ar Livre:\nPrevisão do Tempo: {previsaoTempo}";
    }
}

class Program
{
    static void Main()
    {
        // Criar eventos de cada tipo
        Endereco enderecoEvento = new Endereco("Rua Exemplo", "Cidade Exemplo", "Estado", "12345-678");

        Palestra palestra = new Palestra("Palestra de Exemplo", "Descrição da Palestra", DateTime.Now, new TimeSpan(14, 0, 0), enderecoEvento, "Palestrante Exemplo", 100);

        Recepcao recepcao = new Recepcao("Recepção de Exemplo", "Descrição da Recepção", DateTime.Now.AddDays(7), new TimeSpan(18, 30, 0), enderecoEvento, "confirmacao@exemplo.com");

        ReuniaoAoArLivre reuniaoAoArLivre = new ReuniaoAoArLivre("Reunião ao Ar Livre de Exemplo", "Descrição da Reunião ao Ar Livre", DateTime.Now.AddDays(14), new TimeSpan(10, 0, 0), enderecoEvento, "Ensolarado");

        // Exibir mensagens de marketing para cada evento
        Console.WriteLine(palestra.ObterDetalhesPadrao());
        Console.WriteLine(palestra.ObterDetalhesCompletos());
        Console.WriteLine(palestra.ObterBreveDescricao());
        Console.WriteLine();

        Console.WriteLine(recepcao.ObterDetalhesPadrao());
        Console.WriteLine(recepcao.ObterDetalhesCompletos());
        Console.WriteLine(recepcao.ObterBreveDescricao());
        Console.WriteLine();

        Console.WriteLine(reuniaoAoArLivre.ObterDetalhesPadrao());
        Console.WriteLine(reuniaoAoArLivre.ObterDetalhesCompletos());
        Console.WriteLine(reuniaoAoArLivre.ObterBreveDescricao());
    }
}

using System;
using System.Collections.Generic;

// Interface para ser implementada a classe que irá enviar as notificações:
public interface IMySubject
{
    // Método de inscrição. Aqui tem como parâmentro um objeto da classe Pessoa
    void Subscribe(Pessoa pessoa);

    // Método de remover a inscrição. Aqui tem como parâmentro um objeto da classe Pessoa
    void Unsubscribe(Pessoa pessoa);

    // Método de notificação. Aqui tem como parâmentro uma string
    void Notify(string valor);
}

// Interface para ser implementada a classe que irá receber as notificações:
public interface IMyObservable
{
    // Método que irá receber as notifições
    void Update(string valor);
}

// Classe exemplo que irá enviar as notificações e será implementada com a interface IMySubject:
public class YoutubeChannel : IMySubject
{
    // Propriedade da classe YoutubeChannel
    public string Nome { get; set; }

    // Lista necessário para poder INCLUIR e REMOVER nas inscrições e poder enviar as notificações
    private List<Pessoa> pessoas;

    // Construtor
    public YoutubeChannel(string nome)
    {
        Nome = nome;
        pessoas = new List<Pessoa>();
    }

    // Implementação do método Subscribe da interface IMySubject
    public void Subscribe(Pessoa pessoa)
    {
        Console.WriteLine("Usuário(a) {0} foi inscrito(a) para receber as notificações do Canal {1}", pessoa.Nome, Nome);
        pessoas.Add(pessoa);
    }

    // Implementação do método Unsubscribe da interface IMySubject
    public void Unsubscribe(Pessoa pessoa)
    {
        Console.WriteLine("Usuário(a) {0} foi removido(a) da inscrição do Canal {1}", pessoa.Nome, Nome);
        pessoas.Remove(pessoa);
    }

    // Implementação do método Notify da interface IMySubject
    public void Notify(string valor)
    {
        foreach (var pessoa in pessoas)
        {
            pessoa.Update(valor);
        }
    }

}

// Classe exemplo que irá receber as notificações sendo implementada com a interface IMyObservable
public class Pessoa : IMyObservable
{
    // Propriedade da classe Pessoa
    public string Nome { get; set; }

    // Construtor
    public Pessoa(string nome)
    {
        Nome = nome;
    }

    // Método implementado da interface IMyObservable
    public void Update(string valor)
    {
        // Exibindo a notificação no console
        Console.WriteLine("Usuário(a) {0} recebeu a notificação: {1}", Nome, valor);
    }
}

// Programa
namespace ExemploObservable
{
    // Classe principal do programa
    class Program
    {
        // Método Main
        static void Main(string[] args)
        {
            // Istanciando um objeto com a classe YoutubeChannel
            YoutubeChannel channel = new YoutubeChannel("SauloChannel");

            // Instanciaondo dois objetos com a classe Pessoa
            Pessoa juliana = new Pessoa("Julina");
            Pessoa aline = new Pessoa("Aline");

            // Inscrevendo os objetos
            channel.Subscribe(juliana);
            channel.Subscribe(aline);

            // Enviando uma notificação
            channel.Notify("Enviando notificação 1");
    
            // Desinscrevendo o objeto aline das notificações
            channel.Unsubscribe(aline);

            // Enviando uma nova notifição (agora aline não receberá a notificação pois foi desinscrita)
            channel.Notify("Enviando notificação 2");
        }
    }
}

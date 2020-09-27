using System;
using System.Collections.Generic;

// Interface para ser implementada a classe que irá enviar as notificações:
public interface IMySubject
{
    // Método de inscrição. Aqui tem como parâmentro a interface IMyObservable
    void Subscribe(IMyObservable observer);

    // Método de remover a inscrição. Aqui tem como parâmentro a interface IMyObservable
    void Unsubscribe(IMyObservable observer);

    // Método de notificação. Aqui tem como parâmentro um objeto da classe Pessoa
    void Notify(Pessoa pessoa);
}

// Interface para ser implementada a classe que irá receber as notificações:
public interface IMyObservable
{
    // Método que irá receber as notifições
    void Update(Pessoa pessoa);
}

// Classe exemplo que irá enviar as notificações e será implementada com a interface IMySubject:
public class YoutubeChannel : IMySubject
{
    // Propriedade da classe YoutubeChannel
    public string Nome { get; set; }

    // Lista necessário para poder INCLUIR e REMOVER nas inscrições e poder enviar as notificações
    private List<IMyObservable> Observes;

    // Construtor
    public YoutubeChannel(string nome)
    {
        Nome = nome;
        Observes = new List<IMyObservable>();
    }

    // Implementação do método Subscribe da interface IMySubject
    public void Subscribe(IMyObservable observer)
    {
        Console.WriteLine("Usuário(a) {0} foi inscrito(a) para receber as notificações do Canal {1}", observer, Nome);
        Observes.Add(observer);
    }

    // Implementação do método Unsubscribe da interface IMySubject
    public void Unsubscribe(IMyObservable observer)
    {
        Console.WriteLine("Usuário(a) {0} foi removido(a) da inscrição do Canal {1}", observer, Nome);
        Observes.Remove(observer);
    }

    // Implementação do método Notify da interface IMySubject
    public void Notify(Pessoa pessoa)
    {
        foreach (var observer in Observes)
        {
            observer.Update(pessoa);
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
    public void Update(Pessoa pessoa)
    {
        // Exibindo a notificação no console
        Console.WriteLine("Usuário(a) {0} recebeu a notificação: {1}", Nome, pessoa);
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
            channel.Notify(juliana);

            // Desinscrevendo o objeto aline das notificações
            channel.Unsubscribe(aline);

            // Enviando uma nova notifição (agora aline não receberá a notificação pois foi desinscrita)
            channel.Notify(aline);
        }
    }
}
